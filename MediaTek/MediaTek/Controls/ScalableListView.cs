using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace MediaTek.Controls
{
    public class ScalableListView : ListView
    {
        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Zoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register(
            "Zoom",
            typeof(double),
            typeof(ScalableListView),
            new UIPropertyMetadata(1.0));

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Delta > 0)
                {
                    ZoomIn();
                }
                else if (e.Delta < 0)
                {
                    ZoomOut();
                }
                e.Handled = true;
            }
            base.OnMouseWheel(e);
        }

        public void ZoomOut()
        {
            Zoom /= 1.1;
        }

        public void ZoomIn()
        {
            Zoom *= 1.1;
        }
    }
}
