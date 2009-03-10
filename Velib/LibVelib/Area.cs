﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibVelib
{
    [XmlRoot("arrondissement")]
    public class Area
    {
        [XmlElement("number")]
        public string Number { get; set; }

        [XmlElement("minLat")]
        public double MinLatitude { get; set; }

        [XmlElement("maxLat")]
        public double MaxLatitude { get; set; }

        [XmlElement("minLng")]
        public double MinLongitude { get; set; }

        [XmlElement("maxLng")]
        public double MaxLongitude { get; set; }

    }
}
