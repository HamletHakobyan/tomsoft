using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Controls
{
    public class FormDataView : ItemsControl
    {
        static FormDataView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormDataView),
                                                     new FrameworkPropertyMetadata(typeof(FormDataView)));
        }

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(FormDataView), new UIPropertyMetadata(false));
    }
}
