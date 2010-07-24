using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using SharpDB.ViewModel;
using SharpDB.Model.Data;
using System.Windows.Media.Imaging;

namespace SharpDB.Converters
{
    public class DbItemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as DbModelItemViewModel;
            if (item == null)
                return null;

            string imageName;

            switch (item.ItemType)
            {
                case DbItemType.Table:
                    imageName = "table.png";
                    break;
                case DbItemType.Index:
                    imageName = "primary_key.png";
                    break;
                case DbItemType.Custom:
                    // TODO
                case DbItemType.Column:
                default:
                    return null;
            }

            Uri imageUri = new Uri(string.Format("/Images/{0}", imageName), UriKind.Relative);
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = imageUri;
            img.EndInit();
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
