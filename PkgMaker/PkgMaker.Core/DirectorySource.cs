using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Core
{
    [DebuggerDisplay("DirectorySource [{Path}]")]
    public class DirectorySource : ContentSourceBase
    {
        public DirectorySource()
        {
            Recursive = true;
            Inclusions = new List<FilterBase>();
            Exclusions = new List<FilterBase>();
        }

        [XmlAttribute]
        public string Path { get; set; }
        [XmlAttribute]
        public bool Recursive { get; set; }


        [XmlElement("IncludeDirectory", typeof(DirectoryFilter))]
        [XmlElement("IncludeFile", typeof(FileFilter))]
        [XmlElement("IncludeExtension", typeof(ExtensionFilter))]
        [XmlElement("IncludePattern", typeof(PatternFilter))]
        public List<FilterBase> Inclusions { get; set; }

        [XmlElement("ExcludeDirectory", typeof(DirectoryFilter))]
        [XmlElement("ExcludeFile", typeof(FileFilter))]
        [XmlElement("ExcludeExtension", typeof(ExtensionFilter))]
        [XmlElement("ExcludePattern", typeof(PatternFilter))]
        public List<FilterBase> Exclusions { get; set; }

        public bool IncludeItem(FileSystemInfo item, string basePath, PackageProperties properties)
        {
            // If no inclusions are defined, the item is included unless an exclusion matches
            if (Inclusions.Any())
            {
                // If inclusions are defined, at least one inclusion must match
                if (!Inclusions.Any(e => e.IsMatch(item, basePath, properties)))
                    return false;
            }

            // If any exclusion matches, the item is excluded
            return !Exclusions.Any(e => e.IsMatch(item, basePath, properties));
        }

        public void PrepareFilters(PackageProperties properties)
        {
            foreach (FilterBase filter in Inclusions.Concat(Exclusions))
            {
                filter.Prepare(properties);
            }
        }
    }
}
