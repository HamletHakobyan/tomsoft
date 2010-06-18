using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("ExtensionExclusion [{Extension}]")]
    public class ExtensionExclusion : ExclusionBase
    {
        [XmlText]
        public string Extension { get; set; }
    }
}
