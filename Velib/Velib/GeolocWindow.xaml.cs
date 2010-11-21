using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Velib.Util;
using Velib.ViewModel;

namespace Velib
{
    /// <summary>
    /// Interaction logic for GeolocWindow.xaml
    /// </summary>
    public partial class GeolocWindow : Window
    {
        public GeolocWindow()
        {
            InitializeComponent();
            InitDocument();
        }

        private void InitDocument()
        {
            string exeFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string exeDirectory = System.IO.Path.GetDirectoryName(exeFileName);
            string geolocFileName = System.IO.Path.Combine(exeDirectory, "geoloc.html");
            browser.Source = new Uri(geolocFileName);
        }

        private StationViewModel _station;

        public void GotoLocation(StationViewModel station)
        {
            _station = station;
            if (_station != null)
            {
                this.Title = _station.Name + " - " + _station.Address;
                if (_ready)
                {
                    browser.InvokeScript("gotoLocation", station.Location.Latitude, station.Location.Longitude);
                }
            }
        }

        private bool _ready = false;
        private void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            _ready = true;
            if (_station != null)
                GotoLocation(_station);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }
    }
}
