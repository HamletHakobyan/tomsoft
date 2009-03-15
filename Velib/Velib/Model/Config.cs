using System.Collections.Generic;

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
