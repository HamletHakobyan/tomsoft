using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO;

namespace PkgMaker.Core
{
    [DebuggerDisplay("PackageDirectory [{Name}]")]
    public class PackageDirectory
    {
        public PackageDirectory()
        {
            Sources = new List<ContentSourceBase>();
            SubDirectories = new List<PackageDirectory>();
            Includes = new List<IncludeDirectory>();
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("DirectorySource", typeof(DirectorySource))]
        [XmlElement("FileSource", typeof(FileSource))]
        public List<ContentSourceBase> Sources { get; private set; }

        [XmlElement("Directory")]
        public List<PackageDirectory> SubDirectories { get; private set; }

        [XmlElement("Include")]
        public List<IncludeDirectory> Includes { get; private set; }

        public static PackageDirectory FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return FromStream(stream);
            }
        }

        private static PackageDirectory FromStream(FileStream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(PackageDirectory));
            return (PackageDirectory)xs.Deserialize(stream);
        }

        internal void ProcessIncludes(string basePath, PackageProperties properties)
        {
            if (!Includes.IsNullOrEmpty())
            {
                foreach (var include in Includes)
                {
                    if (include.Source.IsNullOrEmpty())
                    {
                        Trace.TraceWarning("No source specified for include, skipping");
                        continue;
                    }
                    string includeFullPath = PathUtil.GetFullPath(basePath, include.Source);
                    include.Directory = FromFile(includeFullPath);
                    include.Directory.ProcessIncludes(basePath, properties);
                }
            }

            if (!SubDirectories.IsNullOrEmpty())
            {
                foreach (var subDir in SubDirectories)
                {
                    subDir.ProcessIncludes(basePath, properties);
                }
            }
        }
    }
}
