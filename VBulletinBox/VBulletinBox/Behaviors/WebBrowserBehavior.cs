using System.Windows;
using System.Windows.Controls;

namespace VBulletinBox.Behaviors
{
    public static class WebBrowserBehavior
    {
        public static string GetHtmlSource(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlSourceProperty);
        }

        public static void SetHtmlSource(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for HtmlSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlSourceProperty =
            DependencyProperty.RegisterAttached(
              "HtmlSource",
              typeof(string),
              typeof(WebBrowserBehavior),
              new UIPropertyMetadata(
                null,
                HtmlSourceChanged));

        private static void HtmlSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var wb = o as WebBrowser;
            if (wb == null)
                return;
            var newValue = (string)e.NewValue;
            if (newValue != null)
                wb.NavigateToString(newValue);
        }

    }
}
