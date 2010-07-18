using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Developpez.Dotnet;
using System.ComponentModel;
using System.Windows.Shell;

namespace SharpDB.Model
{
    public class Config
    {
        public Config(string filename)
            : this()
        {
            this.FileName = filename;
        }

        public Config()
        {
            this.Connections = new List<DatabaseConnection>();
        }

        [XmlIgnore]
        public IList<DatabaseConnection> Connections { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlArray("Connections")]
        [XmlArrayItem("Connection")]
        public List<DatabaseConnection> ConnectionsXML
        {
            get
            {
                if (Connections != null)
                    return (Connections as List<DatabaseConnection>)
                        ?? Connections.ToList();
                return null;
            }
            set
            {
                Connections = value;
            }
        }

        [XmlArrayItem(typeof(JumpTask))]
        [XmlArrayItem(typeof(JumpPath))]
        public List<JumpItem> JumpListItems { get; set; }

        #region Load and save

        [XmlIgnore]
        public string FileName { get; private set; }

        public static Config FromFile(string filename)
        {
            var xs = new XmlSerializer(typeof(Config));
            using (var reader = new StreamReader(filename))
            {
                var config = (Config)xs.Deserialize(reader);
                config.FileName = filename;
                return config;
            }
        }

        public static string GetDefaultFileName(string applicationName)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string sharpDbDataPath = Path.Combine(appDataPath, applicationName);
            string configFileName = Path.Combine(sharpDbDataPath, "config.xml");
            return configFileName;
        }

        public void Save()
        {
            SaveAs(FileName);
        }

        public void SaveAs(string filename)
        {
            var xs = new XmlSerializer(typeof(Config));
            var path = Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string backupCopy = filename + ".bak";
            if (File.Exists(filename))
            {
                if (File.Exists(backupCopy))
                    File.Delete(backupCopy);
                File.Move(filename, backupCopy);
            }
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    xs.Serialize(writer, this);
                }
            }
            catch
            {
                if (File.Exists(filename))
                    File.Delete(filename);
                if (File.Exists(backupCopy))
                    File.Move(backupCopy, filename);
                throw;
            }
        }

        #endregion
    }
}
