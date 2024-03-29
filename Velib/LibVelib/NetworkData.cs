﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Velib
{
    [XmlRoot("carto")]
    public class NetworkData
    {
        [XmlArray("markers")]
        [XmlArrayItem("marker")]
        public List<Station> Stations { get; set; }

        [XmlArray("arrondissements")]
        [XmlArrayItem("arrondissement")]
        public List<Area> Areas { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public Uri BaseUri { get; set; }

        [XmlElement("BaseUri")]
        public string BaseUriString
        {
            get { return BaseUri.AbsoluteUri; }
            set { BaseUri = new Uri(value); }
        }

        public void InitProvider()
        {
            VelibProvider provider = new VelibProvider(BaseUri);
            foreach (Station s in Stations)
            {
                s.Provider = provider;
            }
        }
    }
}
