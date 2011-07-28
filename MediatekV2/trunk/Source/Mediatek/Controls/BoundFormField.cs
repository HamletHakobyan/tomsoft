using System.Windows;
using System.Windows.Data;

namespace Mediatek.Controls
{
    public abstract class BoundFormField : FormField
    {
        private BindingBase _binding;
        public BindingBase Binding
        {
            get { return _binding; }
            set
            {
                _binding = value;
                SetBinding(ValueProperty, value);
            }
        }

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(TextFormField),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    var display = Template.FindName("PART_Display", this) as FrameworkElement;
        //    var editor = Template.FindName("PART_Editor", this) as FrameworkElement;
        //    if (this.Binding != null)
        //    {
        //        if (display != null)
        //            display.SetBinding(DataContextProperty, this.Binding);
        //        if (editor != null)
        //            editor.SetBinding(DataContextProperty, this.Binding); 
        //    }
        //}
    }

    public class TextFormField : BoundFormField
    {
        static TextFormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TextFormField),
                new FrameworkPropertyMetadata(typeof(TextFormField)));
        }
    }

    public class CheckBoxFormField : BoundFormField
    {
        static CheckBoxFormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxFormField),
                                                     new FrameworkPropertyMetadata(typeof(CheckBoxFormField)));
        }

        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }

        public static readonly DependencyProperty IsThreeStateProperty =
            DependencyProperty.Register("IsThreeState", typeof(bool), typeof(CheckBoxFormField), new UIPropertyMetadata(false));

    }

    public class ComboBoxFormField : BoundFormField
    {
        static ComboBoxFormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxFormField),
                                                     new FrameworkPropertyMetadata(typeof(ComboBoxFormField)));
        }
    }

    public class RatingFormField : BoundFormField
    {
        static RatingFormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingFormField),
                                                     new FrameworkPropertyMetadata(typeof(RatingFormField)));
        }
    }
}