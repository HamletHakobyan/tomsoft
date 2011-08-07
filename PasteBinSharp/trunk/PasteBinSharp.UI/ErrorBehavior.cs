using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PasteBinSharp.UI
{
    public static class ErrorBehavior
    {
        public static string GetErrorMessage(this DependencyObject obj)
        {
            return (string)obj.GetValue(ErrorMessageProperty);
        }

        public static void SetErrorMessage(this DependencyObject obj, string value)
        {
            obj.SetValue(ErrorMessageProperty, value);
        }

        // Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.RegisterAttached(
              "ErrorMessage",
              typeof(string),
              typeof(ErrorBehavior),
              new UIPropertyMetadata(
                null,
                ErrorMessageChanged));



        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(ImageSource), typeof(ErrorAdorner), new UIPropertyMetadata(GetDefaultErrorIcon()));

        private static void ErrorMessageChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UIElement uiElement = o as UIElement;
            if (uiElement == null)
                return;

            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;

            if (!string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(newValue))
            {
                RemoveErrorAdorner(uiElement);
            }
            else if (!string.IsNullOrEmpty(newValue) && string.IsNullOrEmpty(oldValue))
            {
                AddErrorAdorner(uiElement);
            }
        }

        private static void RemoveErrorAdorner(UIElement uiElement)
        {
            var layer = AdornerLayer.GetAdornerLayer(uiElement);
            if (layer == null)
                return;

            var adorners = layer.GetAdorners(uiElement);
            if (adorners == null)
                return;
            var adorner = adorners.OfType<ErrorAdorner>().FirstOrDefault();
            if (adorner != null)
                layer.Remove(adorner);
        }

        private static void AddErrorAdorner(UIElement uiElement)
        {
            var layer = AdornerLayer.GetAdornerLayer(uiElement);
            if (layer == null)
                return;

            var adorner = new ErrorAdorner(uiElement);
            layer.Add(adorner);
        }

        private static ImageSource GetDefaultErrorIcon()
        {
            return new BitmapImage(
                new Uri("pack://application:,,,/PasteBinSharp.UI;component/Images/error.png"));
        }

        private class ErrorAdorner : Adorner
        {
            private readonly Border _errorBorder;

            public ErrorAdorner(UIElement adornedElement) : base(adornedElement)
            {
                _errorBorder = new Border();
                _errorBorder.BorderThickness = new Thickness(2);
                _errorBorder.BorderBrush = Brushes.Red;
                Image img = new Image();
                img.HorizontalAlignment = HorizontalAlignment.Right;
                img.VerticalAlignment = VerticalAlignment.Center;
                Binding imgBinding = new Binding
                    {
                        Source = adornedElement,
                        Path = new PropertyPath(IconProperty)
                    };
                img.SetBinding(Image.SourceProperty, imgBinding);
                Binding ttBinding = new Binding
                    {
                        Source = adornedElement,
                        Path = new PropertyPath(ErrorMessageProperty)
                    };
                img.SetBinding(ToolTipProperty, ttBinding);
                _errorBorder.Child = img;
                this.AddVisualChild(_errorBorder);
                this.AddLogicalChild(_errorBorder);
            }

            protected override Size MeasureOverride(Size constraint)
            {
                AdornedElement.Measure(constraint);
                return AdornedElement.RenderSize;
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                _errorBorder.Arrange(new Rect(finalSize));
                return finalSize;
            }

            protected override Visual GetVisualChild(int index)
            {
                if (index == 0)
                    return _errorBorder;
                throw new ArgumentOutOfRangeException("index");
            }

            protected override int VisualChildrenCount
            {
                get
                {
                    return 1;
                }
            }

            protected override System.Collections.IEnumerator LogicalChildren
            {
                get
                {
                    yield return _errorBorder;
                }
            }
        }
    }
}
