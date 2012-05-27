using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("PropertiesInclude [{Source}]")]
    public class PropertiesInclude
    {
        [XmlAttribute]
        public string Source { get; set; }

        [XmlIgnore]
        public PackageProperties Properties { get; set; }
    }
}
