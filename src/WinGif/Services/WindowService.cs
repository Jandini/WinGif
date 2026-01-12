using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WinGif
{
    internal class WindowService : IWindowService
    {
        private readonly ILogger<WindowService> _logger;

        public WindowService(ILogger<WindowService> logger)
        {
            _logger = logger;
        }

        public void ListWindows()
        {
            _logger.LogInformation("Enumerating all visible windows:");
            
            var windows = GetAllWindows();
            
            if (windows.Count == 0)
            {
                _logger.LogWarning("No visible windows found");
                return;
            }

            foreach (var window in windows)
            {
                _logger.LogInformation("  \"{Title}\"", window);
            }
            
            _logger.LogInformation("Total windows found: {Count}", windows.Count);
        }

        private List<string> GetAllWindows()
        {
            var windows = new List<string>();
            
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    int length = GetWindowTextLength(hWnd);
                    if (length > 0)
                    {
                        StringBuilder builder = new StringBuilder(length + 1);
                        GetWindowText(hWnd, builder, builder.Capacity);
                        string title = builder.ToString();
                        
                        if (!string.IsNullOrWhiteSpace(title))
                        {
                            windows.Add(title);
                        }
                    }
                }
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
    }
}
