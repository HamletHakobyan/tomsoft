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
            var descriptor = DependencyPropertyDescriptor.FromProperty(prop, prop.OwnerType);
            EventHandler handler = (o, e) =>
            {
                settings[SettingName] = obj.GetValue(prop);
            };
            descriptor.AddValueChanged(obj, handler);
            return settings[SettingName];
        }

        private void RegisterForSettingsChanged(DependencyObject obj, DependencyProperty prop)
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(AppSettings.SettingsProperty, obj.GetType());
            
            EventHandler handler = null;
            handler = (o, e) =>
                {
                    var settings = AppSettings.GetSettings(obj);
                    if (settings != null)
                    {
                        var value = GetSettingValue(obj, prop, settings);
                        obj.SetValue(prop, value);
                        descriptor.RemoveValueChanged(obj, handler);
                    }
                };
            descriptor.AddValueChanged(obj, handler);
        }
    }
}
