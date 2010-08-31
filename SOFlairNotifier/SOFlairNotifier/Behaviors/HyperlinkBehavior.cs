using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;

namespace SOFlairNotifier.Behaviors
{
    public static class HyperlinkBehavior
    {
        [AttachedPropertyBrowsableForType(typeof(Hyperlink))]
        public static bool GetOpenInBrowser(Hyperlink hyperlink)
        {
            return (bool)hyperlink.GetValue(OpenInBrowserProperty);
        }

        public static void SetOpenInBrowser(Hyperlink hyperlink, bool value)
        {
            hyperlink.SetValue(OpenInBrowserProperty, value);
        }

        // Using a DependencyProperty as the backing store for OpenInBrowser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenInBrowserProperty =
            DependencyProperty.RegisterAttached(
              "OpenInBrowser",
              typeof(bool),
              typeof(HyperlinkBehavior),
              new UIPropertyMetadata(
                false,
                OpenInBrowserChanged));

        private static void OpenInBrowserChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var hyperlink = o as Hyperlink;
            if (hyperlink == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                hyperlink.RequestNavigate -= hyperlink_RequestNavigate;
            }
            else if (newValue && !oldValue)
            {
                hyperlink.RequestNavigate += hyperlink_RequestNavigate;
            }

        }

        static void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
            e.Handled = true;
        }

    }
}
