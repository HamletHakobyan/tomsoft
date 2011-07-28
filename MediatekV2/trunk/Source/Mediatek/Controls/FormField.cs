using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Controls
{
    [TemplatePart(Name = "PART_Display", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Editor", Type = typeof(FrameworkElement))]
    public class FormField : Control
    {
        static FormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormField),
                                                     new FrameworkPropertyMetadata(typeof(FormField)));
        }

        public object MyStyleKey
        {
            get { return this.DefaultStyleKey; }
        }

            public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(FormField), new UIPropertyMetadata(null));

        public DataTemplate EditorTemplate
        {
            get { return (DataTemplate)GetValue(EditorTemplateProperty); }
            set { SetValue(EditorTemplateProperty, value); }
        }

        public static readonly DependencyProperty EditorTemplateProperty =
            DependencyProperty.Register("EditorTemplate", typeof(DataTemplate), typeof(FormField), new UIPropertyMetadata(null));

        public DataTemplate DisplayTemplate
        {
            get { return (DataTemplate)GetValue(DisplayTemplateProperty); }
            set { SetValue(DisplayTemplateProperty, value); }
        }

        public static readonly DependencyProperty DisplayTemplateProperty =
            DependencyProperty.Register("DisplayTemplate", typeof(DataTemplate), typeof(FormField), new UIPropertyMetadata(null));
    }
}