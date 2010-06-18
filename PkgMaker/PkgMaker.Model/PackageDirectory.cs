using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("PackageDirectory [{Name}]")]
    public class PackageDirectory
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("DirectorySource", typeof(DirectorySource))]
        [XmlElement("FileSource", typeof(FileSource))]
        public List<ContentSourceBase> Sources { get; set; }

        [XmlElement("Directory")]
        public List<PackageDirectory> SubDirectories { get; set; }
    }
}
