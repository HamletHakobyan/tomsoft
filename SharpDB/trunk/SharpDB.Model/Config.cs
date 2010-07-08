using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SharpDB.Model
{
    public class Config
    {
        public Config()
        {
            this.Connections = new List<DatabaseConnection>();
        }

        public IList<DatabaseConnection> Connections { get; set; }

        public static Config FromFile(string filename)
        {
            var xs = new XmlSerializer(typeof(Config));
            using (var reader = new StreamReader(filename))
            {
                return (Config)xs.Deserialize(reader);
            }
        }

        public static string GetDefaultFileName(string applicationName)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string sharpDbDataPath = Path.Combine(appDataPath, applicationName);
            string configFileName = Path.Combine(sharpDbDataPath, "config.xml");
            return configFileName;
        }
    }
}
