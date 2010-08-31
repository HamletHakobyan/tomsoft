using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SOFlairNotifier.Util.Win32
{
    public class API
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);


        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
    }
}
