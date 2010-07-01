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
        {
            this.SettingName = settingName;
        }

        public SettingBinding()
        {
        }

        public string SettingName { get; set; }

        public object DesignValue { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return DesignValue;

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
                            var descriptor = DependencyPropertyDescriptor.FromProperty(prop, prop.OwnerType);
                            EventHandler handler = (o, e) =>
                                {
                                    settings[SettingName] = obj.GetValue(prop);
                                };
                            descriptor.AddValueChanged(obj, handler);
                            return settings[SettingName];
                        }
                    }
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
