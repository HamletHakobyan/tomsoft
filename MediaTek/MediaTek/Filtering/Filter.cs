using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;

namespace MediaTek.Filtering
{
    public abstract class Filter : DependencyObject
    {
        public bool Evaluate(object target)
        {
            PropertyInfo pi = target.GetType().GetProperty(this.Property);
            object propertyValue = pi.GetValue(target, null);
            return EvaluateInternal(propertyValue);
        }

        protected abstract bool EvaluateInternal(object propertyValue);

        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Enabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.Register("Enabled", typeof(bool), typeof(Filter), new UIPropertyMetadata(false));


        public bool Negate
        {
            get { return (bool)GetValue(NegateProperty); }
            set { SetValue(NegateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Negate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NegateProperty =
            DependencyProperty.Register("Negate", typeof(bool), typeof(Filter), new UIPropertyMetadata(false));


        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(Filter), new UIPropertyMetadata(null));

        public string Property
        {
            get { return (string)GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Property.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.Register("Property", typeof(string), typeof(Filter), new UIPropertyMetadata(null));
    }
}
