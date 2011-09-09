using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace SharpDB.Util
{
    public static class WindowSettings
    {
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool? GetMaximizeBox(DependencyObject obj)
        {
            return (bool?)obj.GetValue(MaximizeBoxProperty);
        }

        public static void SetMaximizeBox(DependencyObject obj, bool? value)
        {
            obj.SetValue(MaximizeBoxProperty, value);
        }

        // Using a DependencyProperty as the backing store for MaximizeBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximizeBoxProperty =
            DependencyProperty.RegisterAttached(
              "MaximizeBox",
              typeof(bool?),
              typeof(WindowSettings),
              new UIPropertyMetadata(
                null,
                MaximizeBoxChanged));

        private static void MaximizeBoxChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var window = o as Window;
            if (window == null)
                return;

            var oldValue = (bool?)e.OldValue;
            var newValue = (bool?)e.NewValue;

            if (newValue.HasValue)
            {
                SetWindowFlag(window, GWL_STYLE, WS_MAXIMIZEBOX, newValue.Value);
            }
        }

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool? GetMinimizeBox(DependencyObject obj)
        {
            return (bool?)obj.GetValue(MinimizeBoxProperty);
        }

        public static void SetMinimizeBox(DependencyObject obj, bool? value)
        {
            obj.SetValue(MinimizeBoxProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinimizeBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimizeBoxProperty =
            DependencyProperty.RegisterAttached(
              "MinimizeBox",
              typeof(bool?),
              typeof(WindowSettings),
              new UIPropertyMetadata(
                null,
                MinimizeBoxChanged));

        private static void MinimizeBoxChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var window = o as Window;
            if (window == null)
                return;

            var oldValue = (bool?)e.OldValue;
            var newValue = (bool?)e.NewValue;

            if (newValue.HasValue)
            {
                SetWindowFlag(window, GWL_STYLE, WS_MINIMIZEBOX, newValue.Value);
            }
        }


        private static void SetWindowFlag(Window window, int nIndex, int flags, bool isSet)
        {
            var helper = new WindowInteropHelper(window);
            var handle = helper.EnsureHandle();
            int value = GetWindowLong(handle, nIndex);
            if (isSet)
                value = value | flags;
            else
                value = value & ~flags;
            SetWindowLong(handle, nIndex, value);
        }

        #region Win32 Interop

        const int WS_MINIMIZEBOX = 0x20000;
        const int WS_MAXIMIZEBOX = 0x10000;
        const int GWL_STYLE = -16;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        #endregion

    }
}
