using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Velib.Util
{
    public struct LatLngPoint
    {
        private double _latitude;
        private double _longitude;

        public double Latitude
        {
            get { return _latitude; }
        }
        public double Longitude
        {
            get { return _longitude; }
        }

        public LatLngPoint(double lat, double lng)
        {
            _latitude = lat;
            _longitude = lng;
        }
    }
}
