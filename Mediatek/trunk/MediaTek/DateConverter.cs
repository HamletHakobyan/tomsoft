using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace MediaTek
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string format = "F";
            if (parameter != null)
                format = parameter.ToString();
            DateTime date = (DateTime)value;
            return date.ToString(format, culture.DateTimeFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, culture.DateTimeFormat, DateTimeStyles.None, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }

}
