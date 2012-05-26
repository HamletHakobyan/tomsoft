using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("IncludeProperties [{Source}]")]
    public class IncludeProperties
    {
        [XmlAttribute]
        public string Source { get; set; }

        [XmlIgnore]
        public PackageProperties Properties { get; set; }
    }
}
