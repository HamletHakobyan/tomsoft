using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibVelib
{
    [XmlRoot("carto")]
    public class StationList
    {
        [XmlArray("markers")]
        [XmlArrayItem("marker")]
        public List<Station> Stations { get; set; }

        [XmlArray("arrondissements")]
        [XmlArrayItem("arrondissement")]
        public List<Area> Areas { get; set; }
    }
}
