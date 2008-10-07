using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MediaTek.Utilities;

namespace MediaTek.Controls
{
    public class FilterTextBox : TextBox
    {
        public bool IsFilterSet
        {
            get { return (bool)GetValue(IsFilterSetProperty); }
        }

        // Using a DependencyProperty as the backing store for IsFilterSet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFilterSetProperty =
            DependencyProperty.Register("IsFilterSet", typeof(bool), typeof(FilterTextBox), new UIPropertyMetadata(false));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            SetValue(IsFilterSetProperty, !string.IsNullOrEmpty(this.Text));
            OnFilterChanged();
        }

        public event RoutedEventHandler FilterChanged;

        protected void OnFilterChanged()
        {
            if (FilterChanged != null)
                FilterChanged(this, new RoutedEventArgs());
        }

        public Predicate<object> Filter
        {
            get
            {
                return IsMatch;
            }
        }

        protected bool IsMatch(object obj)
        {
            if (!IsFilterSet) return true;
            if (obj is IFilterable)
            {
                return (obj as IFilterable).IsMatch(this.Text);
            }
            else
            {
                throw new ArgumentException("The object to filter must implement IFilterable");
            }
        }
    }
}
