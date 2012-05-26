using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Core
{
    [DebuggerDisplay("Package [{Name}]")]
    public class Package
    {
        public Package()
        {
            Properties = new PackageProperties();
            Root = new PackageDirectory();
        }

        [XmlIgnore]
        public string FileName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string OutputFileName { get; set; }
        public string SourceBasePath { get; set; }

        public PackageProperties Properties { get; set; }

        public PackageDirectory Root { get; set; }

        #region Load methods

        public static Package FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var pkg = FromStream(stream);
                pkg.FileName = fileName;

                string basePath = Path.GetDirectoryName(Path.GetFullPath(fileName));
                
                pkg.Properties.ProcessIncludes(basePath);
                pkg.Properties.LoadValues();

                pkg.Root.ProcessIncludes(basePath, pkg.Properties);

                return pkg;
            }
        }

        private static Package FromStream(Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Package));
            return (Package)xs.Deserialize(stream);
        }

        #endregion

        #region Save methods

        public void Save(string fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                Save(stream);
            }
        }

        private void Save(FileStream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Package));
            xs.Serialize(stream, this);
        }

        #endregion

        public string ExpandProperties(string text)
        {
            return Properties.Expand(text);
        }
    }

}
