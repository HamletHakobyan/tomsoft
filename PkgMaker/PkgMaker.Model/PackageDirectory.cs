using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO;
using Developpez.Dotnet;
using Developpez.Dotnet.Collections;

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

        [XmlElement("Include")]
        public List<IncludeDirectory> Includes { get; set; }

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

        internal void ProcessIncludes(string basePath)
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
                    include.Directory.ProcessIncludes(basePath);
                }
            }

            if (!SubDirectories.IsNullOrEmpty())
            {
                foreach (var subDir in SubDirectories)
                {
                    subDir.ProcessIncludes(basePath);
                }
            }
        }
    }
}
