using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("IncludeDirectory [{Name} (from {Source})]")]
    public class IncludeDirectory
    {
        [XmlAttribute]
        public string Source { get; set; }

        [XmlIgnore]
        public PackageDirectory Directory { get; set; }
    }
}
