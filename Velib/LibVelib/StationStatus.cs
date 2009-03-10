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
        public int Available { get; set; }

        [XmlElement("free")]
        public int Free { get; set; }

        [XmlElement("total")]
        public int Total { get; set; }

        [XmlElement("ticket")]
        public int Ticket { get; set; }
    }
}
