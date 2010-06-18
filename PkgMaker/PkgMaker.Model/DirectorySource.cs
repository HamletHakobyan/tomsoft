using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("DirectorySource [{Path}]")]
    public class DirectorySource : ContentSourceBase
    {
        public DirectorySource()
        {
            Recursive = true;
        }

        [XmlAttribute]
        public string Path { get; set; }
        [XmlAttribute]
        public bool Recursive { get; set; }

        [XmlElement("ExcludeDirectory", typeof(DirectoryExclusion))]
        [XmlElement("ExcludeFile", typeof(FileExclusion))]
        [XmlElement("ExcludeExtension", typeof(ExtensionExclusion))]
        [XmlElement("ExcludePattern", typeof(PatternExclusion))]
        public List<ExclusionBase> Exclusions { get; set; }
    }
}
