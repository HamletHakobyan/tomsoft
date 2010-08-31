using System;
using System.Windows;
using System.Windows.Interop;
using SOFlairNotifier.Util.Win32;

namespace SOFlairNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            var style = (WindowStyles)API.GetWindowLong(helper.Handle, API.GWL_STYLE);
            style &= ~WindowStyles.WS_MAXIMIZEBOX;
            API.SetWindowLong(helper.Handle, API.GWL_STYLE, (int)style);
            var source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(MainWindow_WndProc);
        }

        IntPtr MainWindow_WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x84) // WM_NCHITTEST
            {
                int x = lParam.ToInt32() & 0x0000FFFF;
                int y = (lParam.ToInt32() & 0x7FFF0000) >> 16;
                var p = PointFromScreen(new Point(x, y));
                var child = (FrameworkElement)GetVisualChild(0);
                Rect clientRect = new Rect(Padding.Left, Padding.Top, child.ActualWidth + Padding.Right, child.ActualHeight + Padding.Bottom);
                if (clientRect.Contains(p))
                {
                    handled = true;
                    return new IntPtr(1); // HTCLIENT
                }
                else
                {
                    handled = true;
                    return new IntPtr(2); // HTCAPTION
                }
            }
            return IntPtr.Zero;
        }
    }
}
