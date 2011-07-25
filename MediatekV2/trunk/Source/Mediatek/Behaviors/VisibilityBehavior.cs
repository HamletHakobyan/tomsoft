using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Behaviors
{
    public static class VisibilityBehavior
    {
        public static bool GetHideIfEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideIfEmptyProperty);
        }

        public static void SetHideIfEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(HideIfEmptyProperty, value);
        }

        public static readonly DependencyProperty HideIfEmptyProperty =
            DependencyProperty.RegisterAttached(
              "HideIfEmpty",
              typeof(bool),
              typeof(VisibilityBehavior),
              new UIPropertyMetadata(
                false,
                HideIfEmptyChanged));

        private static void HideIfEmptyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = o as UIElement;
            if (element == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                Detach(element);
            }
            else if (newValue && !oldValue)
            {
                Attach(element);
            }
        }

        private static void Attach(UIElement element)
        {
            var handler = GetEventHandler(element);
            var descriptor = GetPropertyDescriptor(element);
            if (handler == null || descriptor == null)
                return;
            descriptor.AddValueChanged(element, handler);
        }

        private static void Detach(UIElement element)
        {
            var handler = GetEventHandler(element);
            var descriptor = GetPropertyDescriptor(element);
            if (handler == null || descriptor == null)
                return;
            descriptor.RemoveValueChanged(element, handler);
        }

        private static EventHandler GetEventHandler(UIElement element)
        {
            if (element is TextBlock)
                return textBlock_TextChanged;

            if (element is Image)
               return image_SourceChanged;
            
            if (element is ContentControl)
                return contentControl_ContentChanged;

            return null;
        }

        private static DependencyPropertyDescriptor GetPropertyDescriptor(UIElement element)
        {
            DependencyProperty property = null;

            if (element is TextBlock)
                property = TextBlock.TextProperty;
            else if (element is Image)
                property = Image.SourceProperty;
            else if (element is ContentControl)
                property = ContentControl.ContentProperty;

            if (property != null)
                return DependencyPropertyDescriptor.FromProperty(property, element.GetType());
            
            return null;
        }

        private static void textBlock_TextChanged(object sender, EventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock == null)
                return;
            
            textBlock.Visibility = textBlock.Text.Length > 0
                                       ? Visibility.Visible
                                       : Visibility.Collapsed;
        }

        private static void image_SourceChanged(object sender, EventArgs e)
        {
            Image image = sender as Image;
            if (image == null)
                return;

            image.Visibility = image.Source != null
                                   ? Visibility.Visible
                                   : Visibility.Collapsed;
        }

        private static void contentControl_ContentChanged(object sender, EventArgs e)
        {
            ContentControl contentControl = sender as ContentControl;
            if (contentControl == null)
                return;

            contentControl.Visibility = contentControl.HasContent
                                            ? Visibility.Visible
                                            : Visibility.Collapsed;
        }

    }
}
