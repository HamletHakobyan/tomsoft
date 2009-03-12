using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Velib.Model
{
    public class Config
    {
        public Config()
        {
            this.Networks = new List<Network>();
        }

        public List<Network> Networks { get; set; }

        public string CurrentNetwork { get; set; }
    }
}
