using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Data;
using System.Windows;
using Velib.ViewModel;

namespace Velib.Util
{
    public class GeolocCommandExtension : Binding
    {
        private static GeolocWindow _geolocWindow = null;

        public GeolocCommandExtension()
        {
            Initialize();
        }

        public GeolocCommandExtension(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Converter = GeolocCommandConverter.Instance;
        }

        private class GeolocCommandConverter : IValueConverter
        {
            private GeolocCommandConverter()
            {
            }

            private static GeolocCommandConverter _instance = null;
            public static GeolocCommandConverter Instance
            {
                get
                {
                    if (_instance == null)
                        _instance = new GeolocCommandConverter();
                    return _instance;
                }
            }

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is StationViewModel)
                {
                    var station = (StationViewModel)value;
                    return new RelayCommand(
                        (param) =>
                        {
                            if (_geolocWindow == null)
                            {
                                _geolocWindow = new GeolocWindow();
                            }
                            _geolocWindow.Show();
                            _geolocWindow.GotoLocation(station);
                            _geolocWindow.Activate();
                        });

                }
                else
                {
                    return null;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }

            #endregion
        }
    }
}
