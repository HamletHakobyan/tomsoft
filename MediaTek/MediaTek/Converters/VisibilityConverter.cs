using System;
using System.Windows;
using System.Windows.Data;

namespace MediaTek.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility), ParameterType=typeof(Visibility))]
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility notVisible = Visibility.Collapsed;
            if (parameter != null) notVisible = (Visibility)Enum.Parse(typeof(Visibility), parameter.ToString());
            if ((bool)value)
                return Visibility.Visible;
            else
                return notVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible);
        }
    }
}
