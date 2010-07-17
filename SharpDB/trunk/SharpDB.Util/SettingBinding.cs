using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using Developpez.Dotnet;
using System.Windows;
using System.ComponentModel;

namespace SharpDB.Util
{
    public class SettingBinding : MarkupExtension
    {

        public SettingBinding(string settingName)
            : this()
        {
            this.SettingName = settingName;
        }

        public SettingBinding()
        {
            FallbackValue = DependencyProperty.UnsetValue;
        }

        public string SettingName { get; set; }

        public object FallbackValue { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (SettingName != null)
            {
                var target = serviceProvider.GetService<IProvideValueTarget>();
                if (target != null)
                {
                    var obj = target.TargetObject as DependencyObject;
                    var prop = target.TargetProperty as DependencyProperty;
                    if (obj != null && prop != null)
                    {
                        var settings = AppSettings.GetSettings(obj);
                        if (settings != null)
                        {
                            return GetSettingValue(obj, prop, settings);
                        }
                        else
                        {
                            // In case AppSettings.Settings is set after the markup extension is evaluated
                            RegisterForSettingsChanged(obj, prop);
                        }
                    }
                    if (FallbackValue != null && FallbackValue != DependencyProperty.UnsetValue)
                    {
                        var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                        if (converter != null && converter.CanConvertFrom(FallbackValue.GetType()))
                        {
                            return converter.ConvertFrom(FallbackValue);
                        }
                    }
                }
            }
            
            return FallbackValue;
        }

        private object GetSettingValue(DependencyObject obj, DependencyProperty prop, System.Configuration.ApplicationSettingsBase settings)
        {
            if (!DesignerProperties.GetIsInDesignMode(obj))
            {
                // Track property value to update settings
                var descriptor = DependencyPropertyDescriptor.FromProperty(prop, prop.OwnerType);
                EventHandler handler = (o, e) =>
                {
                    var value = obj.GetValue(prop);
                    if (!object.Equals(settings[SettingName], value))
                        settings[SettingName] = value;
                };
                descriptor.AddValueChanged(obj, handler);

                // Track setting value to update property
                settings.PropertyChanged += (o, e) =>
                {
                    if (string.Equals(e.PropertyName, SettingName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var settingValue = settings[SettingName];
                        if (!object.Equals(obj.GetValue(prop), settingValue))
                            obj.SetValue(prop, settingValue);
                    }
                };
            }
            return settings[SettingName];
        }

        private void RegisterForSettingsChanged(DependencyObject obj, DependencyProperty prop)
        {
            var uiElement = obj as UIElement;
            if (uiElement == null)
                return;

            RoutedEventHandler handler = null;
            handler = (o, e) =>
                {
                    var settings = AppSettings.GetSettings(obj);
                    if (settings != null)
                    {
                        var descriptor = DependencyPropertyDescriptor.FromProperty(AppSettings.SettingsProperty, obj.GetType());
                        var value = GetSettingValue(obj, prop, settings);
                        obj.SetValue(prop, value);
                        uiElement.RemoveHandler(AppSettings.SettingsChangedEvent, handler);
                    }
                };
            uiElement.AddHandler(AppSettings.SettingsChangedEvent, handler);
        }
    }
}
