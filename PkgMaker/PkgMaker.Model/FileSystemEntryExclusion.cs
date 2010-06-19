using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PkgMaker.Model
{
    public abstract class FileSystemEntryExclusion : ExclusionBase
    {
        [XmlText]
        public string Path { get; set; }
        
        [XmlAttribute]
        public bool Any { get; set; }

        public abstract bool Directory { get; }
    }
}
