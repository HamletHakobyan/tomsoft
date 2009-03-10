using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibVelib
{
    [XmlRoot("marker")]
    public class Station
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("number")]
        public string Number { get; set; }

        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("fullAddress")]
        public string FullAddress { get; set; }

        [XmlElement("lat")]
        public double Latitude { get; set; }

        [XmlElement("lng")]
        public double Longitude { get; set; }

        [XmlElement("open")]
        public bool Open { get; set; }

        [XmlElement("bonus")]
        public bool Bonus { get; set; }
    }
}
