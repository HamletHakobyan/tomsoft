using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Velib
{
    [XmlRoot("station")]
    public class StationStatus
    {
        [XmlElement("available")]
        public int AvailableBikes { get; set; }

        [XmlElement("free")]
        public int FreeSlots { get; set; }

        [XmlElement("total")]
        public int TotalSlots { get; set; }

        [XmlElement("ticket")]
        public bool Ticket { get; set; }
    }
}
