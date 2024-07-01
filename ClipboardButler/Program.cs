using System;
using System.Runtime.InteropServices;
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
        private static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG lpMsg);

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        static async Task Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide the console window
            ShowWindow(handle, SW_HIDE);

            // Add to system tray
            IntPtr hWnd = TrayManager.CreateMessageWindow();
            TrayManager.AddToSystemTray(hWnd);

            // Run the clipboard monitoring task
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        string clipboardText = ClipboardManager.GetClipboardText();
                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            if (TextCleaner.TryClean(clipboardText, out var cleanText))
                            {
                                ClipboardManager.SetClipboardText(cleanText);
                            }
                        }
                    }
                    finally
                    {
                        await Task.Delay(50);
                    }
                }
            });

            // Message loop
            MSG msg;
            while (GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }
    }
}
