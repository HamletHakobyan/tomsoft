using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("Property [{Name}: {Value}]")]
    public class PackageProperty
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}
