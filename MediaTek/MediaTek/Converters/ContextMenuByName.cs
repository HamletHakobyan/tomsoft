using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;

namespace MediaTek.Converters
{
    [ValueConversion(typeof(string), typeof(ContextMenu))]
    public class ContextMenuByName : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string key = value as string;
                ContextMenu mnu = App.Current.FindResource(key) as ContextMenu;
                return mnu;
            }
            else
            {
                throw new ArgumentException("The value must be a string");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
