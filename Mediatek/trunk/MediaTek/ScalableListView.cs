using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

namespace MediaTek
{
    public class ScalableListView : ListView
    {
        public ScalableListView()
        {
            bool designMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
            if (!designMode)
            {
                this.ItemContainerStyle = FindResource("stlItemContainer") as Style;
                this.Template = FindResource("tplWrapListView") as ControlTemplate;
            }
        }

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
                    Zoom *= 1.1;
                }
                else if (e.Delta < 0)
                {
                    Zoom /= 1.1;
                }
                e.Handled = true;
            }
            base.OnMouseWheel(e);
        }
    }
}
