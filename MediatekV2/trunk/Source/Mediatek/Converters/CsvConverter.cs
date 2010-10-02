using System;
using System.Collections;
using System.Windows.Data;
using Developpez.Dotnet.Collections;

namespace Mediatek.Converters
{
    [ValueConversion(typeof(IEnumerable), typeof(string))]
    public class CsvConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable collection = value as IEnumerable;
            return collection == null
                ? null
                : collection.ToCsvString(", ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
