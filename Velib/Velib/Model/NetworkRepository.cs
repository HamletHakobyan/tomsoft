using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Velib.Model
{
    public class NetworkRepository
    {
        public NetworkRepository()
        {
            this.Networks = new List<Network>();
        }

        [XmlElement("Networks")]
        public List<Network> Networks { get; set; }

        public string CurrentNetwork { get; set; }
    }
}
