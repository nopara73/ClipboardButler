using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ClipboardButler
{
    internal class ClipboardManager
    {
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

        private const uint CF_UNICODETEXT = 13;
        private const uint GMEM_MOVEABLE = 0x0002;

        public static string GetClipboardText()
        {
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
            {
                return null;
            }

            if (!OpenClipboard(IntPtr.Zero))
            {
                return null;
            }

            IntPtr handle = GetClipboardData(CF_UNICODETEXT);
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

            string clipboardText = Marshal.PtrToStringUni(pointer);
            GlobalUnlock(handle);
            CloseClipboard();

            return clipboardText;
        }

        public static void SetClipboardText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                return;
            }

            IntPtr hGlobal = IntPtr.Zero;
            IntPtr hMem = IntPtr.Zero;
            try
            {
                int bytes = (text.Length + 1) * 2; // UTF-16, hence * 2
                hGlobal = Marshal.StringToHGlobalUni(text);
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
                    Marshal.Copy(text.ToCharArray(), 0, pointer, text.Length);
                    Marshal.WriteInt16(pointer, text.Length * 2, 0); // Null-terminate the string
                }
                finally
                {
                    GlobalUnlock(hMem);
                }

                if (SetClipboardData(CF_UNICODETEXT, hMem) == IntPtr.Zero)
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
