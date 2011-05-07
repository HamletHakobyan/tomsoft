using System.Linq;
using System.Xml.Serialization;

namespace Millionaire.Model
{
    public class Joker
    {
        public Joker()
        {
        }

        public Joker(JokerType type)
        {
            this.Type = type;
        }

        [XmlAttribute]
        public JokerType Type { get; set; }

        [XmlIgnore]
        public bool Used { get; set; }
    }
}
