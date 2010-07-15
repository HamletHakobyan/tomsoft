using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Configuration;

namespace SharpDB.Util
{
    public static class AppSettings
    {
        public static ApplicationSettingsBase GetSettings(DependencyObject obj)
        {
            return (ApplicationSettingsBase)obj.GetValue(SettingsProperty);
        }

        public static void SetSettings(DependencyObject obj, ApplicationSettingsBase value)
        {
            obj.SetValue(SettingsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Settings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.RegisterAttached(
                "Settings",
                typeof(ApplicationSettingsBase),
                typeof(AppSettings),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits,
                    SettingsChanged));

        private static void SettingsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = o as UIElement;
            if (uiElement == null)
                return;
            uiElement.RaiseEvent(new RoutedEventArgs(SettingsChangedEvent));
        }

        public static RoutedEvent SettingsChangedEvent =
            EventManager.RegisterRoutedEvent(
                "SettingsChanged",
                RoutingStrategy.Direct,
                typeof(RoutedEventHandler),
                typeof(AppSettings));

    }
}
