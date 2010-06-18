using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Developpez.Dotnet.Collections;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("Package [{Name}]")]
    public class Package
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string OutputFileName { get; set; }
        public string BasePath { get; set; }

        public PackageDirectory Root { get; set; }

        #region Load methods

        public static Package FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return FromStream(stream);
            }
        }

        public static Package FromStream(Stream stream)
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

    }

}
