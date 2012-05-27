using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("ExtensionFilter [{Extension}]")]
    public class ExtensionFilter : FilterBase
    {
        [XmlText]
        public string Extension { get; set; }

        public override FilterTarget Target
        {
            get { return FilterTarget.File; }
        }

        protected override bool IsMatchCore(FileSystemInfo item, string basePath)
        {
            if (base.IsMatchCore(item, basePath))
            {
                var extensions = GetPossibleExtensions(item.Name);
                return extensions.Any(e => e.TrimStart('.').Equals(_preparedExtension, StringComparison.CurrentCultureIgnoreCase));
            }
            return false;
        }

        private string _preparedExtension;

        public override void Prepare(PackageProperties properties)
        {
            base.Prepare(properties);
            _preparedExtension = properties.Expand(Extension).TrimStart('.');
        }

        private IEnumerable<string> GetPossibleExtensions(string fileName)
        {
            string fullExtension = string.Empty;
            string extension = Path.GetExtension(fileName);
            while (!string.IsNullOrEmpty(extension))
            {
                fullExtension = extension + fullExtension;
                yield return fullExtension;
                fileName = Path.GetFileNameWithoutExtension(fileName);
                extension = Path.GetExtension(fileName);
            }
        }

        public override string ToString()
        {
            return string.Format("ExtensionFilter [{0}]", Extension);
        }
    }
}