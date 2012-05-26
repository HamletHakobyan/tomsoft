using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    public class PackageProperties
    {
        private readonly Dictionary<string, string> _values;

        public PackageProperties()
        {
            Entries = new List<PackageProperty>();
            Includes = new List<IncludeProperties>();
            _values = new Dictionary<string, string>();
        }

        [XmlElement("Property")]
        public List<PackageProperty> Entries { get; private set; }

        [XmlElement("Include")]
        public List<IncludeProperties> Includes { get; private set; }

        [XmlIgnore]
        public IEnumerable<string> Keys
        {
            get { return _values.Keys; }
        }

        public string this[string name]
        {
            get
            {
                string value;
                if (_values.TryGetValue(name, out value))
                    return value;
                return null;
            }
            set { _values[name] = value; }
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
                    include.Properties = FromFile(includeFullPath);
                    include.Properties.ProcessIncludes(basePath);
                }
            }
        }

        public static PackageProperties FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return FromStream(stream);
            }
        }

        private static PackageProperties FromStream(FileStream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(PackageProperties));
            return (PackageProperties)xs.Deserialize(stream);
        }

        internal void LoadValues()
        {
            foreach (var include in Includes)
            {
                LoadValues(include.Properties);
            }
            LoadValues(this);
        }

        private void LoadValues(PackageProperties properties)
        {
            foreach (var entry in properties.Entries)
            {
                _values[entry.Name] = Expand(entry.Value);
            }
        }

        public string Expand(string text)
        {
            if (text == null)
                return null;

            foreach (var kvp in _values)
            {
                string value = kvp.Value;
                if (value == null)
                    continue;
                string pattern = string.Format("{{{0}}}", kvp.Key);
                text = text.Replace(pattern, value);
            }
            return text;
        }
    }
}
