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

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        static async Task Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            // Add to system tray
            IntPtr hWnd = TrayManager.CreateMessageWindow();
            TrayManager.AddToSystemTray(hWnd);

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
        }
    }
}
