using System.Windows;
using System.Windows.Controls;

namespace Velib.Controls
{
    public class DualHeaderGroupBox : ContentControl
    {
        static DualHeaderGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DualHeaderGroupBox), new FrameworkPropertyMetadata(typeof(DualHeaderGroupBox)));
        }

        public object LeftHeader
        {
            get { return (object)GetValue(LeftHeaderProperty); }
            set { SetValue(LeftHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftHeaderProperty =
            DependencyProperty.Register("LeftHeader", typeof(object), typeof(DualHeaderGroupBox), new UIPropertyMetadata(null));

        public object RightHeader
        {
            get { return (object)GetValue(RightHeaderProperty); }
            set { SetValue(RightHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightHeaderProperty =
            DependencyProperty.Register("RightHeader", typeof(object), typeof(DualHeaderGroupBox), new UIPropertyMetadata(null));

    }
}
