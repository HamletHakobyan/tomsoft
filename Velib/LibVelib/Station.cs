using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Velib
{
    [XmlRoot("marker")]
    public class Station
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("number")]
        public string Number { get; set; }

        [XmlAttribute("address")]
        public string Address { get; set; }

        [XmlAttribute("fullAddress")]
        public string FullAddress { get; set; }

        [XmlAttribute("lat")]
        public double Latitude { get; set; }

        [XmlAttribute("lng")]
        public double Longitude { get; set; }

        [XmlAttribute("open")]
        public bool Open { get; set; }

        [XmlAttribute("bonus")]
        public bool Bonus { get; set; }

        internal VelibProvider Provider { get; set; }

        public StationStatus GetStatus()
        {
            if (Provider != null)
            {
                return Provider.GetStatus(this);
            }
            else
            {
                return new StationStatus
                {
                    AvailableBikes = -1,
                    FreeSlots = -1,
                    TotalSlots = -1,
                    Ticket = false
                };
            }
        }
    }
}
