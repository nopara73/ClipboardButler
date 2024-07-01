using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardButler
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        private static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const uint CF_TEXT = 1;
        private const uint GMEM_MOVEABLE = 0x0002;

        static async Task Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_SHOW);

            while (true)
            {
                try
                {
                    string clipboardText = GetClipboardText();
                    if (!string.IsNullOrEmpty(clipboardText))
                    {
                        string cleanText = clipboardText.ToUpper();
                        SetClipboardText(cleanText);
                    }
                }
                finally
                {
                    await Task.Delay(50);

                }
            }
        }

        private static string GetClipboardText()
        {
            if (!IsClipboardFormatAvailable(CF_TEXT))
            {
                return null;
            }

            if (!OpenClipboard(IntPtr.Zero))
            {
                return null;
            }

            IntPtr handle = GetClipboardData(CF_TEXT);
            if (handle == IntPtr.Zero)
            {
                CloseClipboard();
                return null;
            }

            IntPtr pointer = GlobalLock(handle);
            if (pointer == IntPtr.Zero)
            {
                CloseClipboard();
                return null;
            }

            string clipboardText = Marshal.PtrToStringAnsi(pointer);
            GlobalUnlock(handle);
            CloseClipboard();

            return clipboardText;
        }

        private static void SetClipboardText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                return;
            }

            IntPtr hGlobal = IntPtr.Zero;
            IntPtr hMem = IntPtr.Zero;
            try
            {
                int bytes = (text.Length + 1) * Marshal.SystemMaxDBCSCharSize;
                hGlobal = Marshal.StringToHGlobalAnsi(text);
                if (hGlobal == IntPtr.Zero)
                {
                    throw new Exception("Failed to allocate memory for clipboard text.");
                }

                hMem = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)bytes);
                if (hMem == IntPtr.Zero)
                {
                    throw new Exception("Failed to allocate global memory.");
                }

                IntPtr pointer = GlobalLock(hMem);
                if (pointer == IntPtr.Zero)
                {
                    throw new Exception("Failed to lock global memory.");
                }

                try
                {
                    // Copy the ANSI string into the pointer manually
                    byte[] buffer = Encoding.Default.GetBytes(text);
                    Marshal.Copy(buffer, 0, pointer, buffer.Length);
                    Marshal.WriteByte(pointer, buffer.Length, 0); // Null-terminate the string
                }
                finally
                {
                    GlobalUnlock(hMem);
                }

                if (SetClipboardData(CF_TEXT, hMem) == IntPtr.Zero)
                {
                    throw new Exception("Failed to set clipboard data.");
                }

                hMem = IntPtr.Zero; // Prevent memory from being freed by GlobalFree
            }
            finally
            {
                if (hGlobal != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hGlobal);
                }

                if (hMem != IntPtr.Zero)
                {
                    GlobalFree(hMem);
                }

                CloseClipboard();
            }
        }

    }
}
