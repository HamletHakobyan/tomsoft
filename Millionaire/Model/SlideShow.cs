using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Millionaire.Model
{
    public class SlideShow : Slide
    {
        [XmlElement("Photo")]
        public List<string> Photos { get; set; }
        public int Interval { get; set; }
        public string SoundPath { get; set; }
    }
}
