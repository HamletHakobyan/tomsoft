using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("DirectoryExclusion [{Path}]")]
    public class DirectoryExclusion : ExclusionBase
    {
        [XmlText]
        public string Path { get; set; }
    }
}
