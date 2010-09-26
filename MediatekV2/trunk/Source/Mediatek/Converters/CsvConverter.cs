using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using Developpez.Dotnet.Collections;

namespace Mediatek.Converters
{
    [ValueConversion(typeof(IEnumerable), typeof(string))]
    public class CsvConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable collection = value as IEnumerable;
            if (collection == null)
                return null;
            return collection.ToCsvString(", ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
