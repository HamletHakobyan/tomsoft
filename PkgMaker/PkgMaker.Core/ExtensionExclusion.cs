using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO;

namespace PkgMaker.Core
{
    [DebuggerDisplay("ExtensionExclusion [{Extension}]")]
    public class ExtensionExclusion : ExclusionBase
    {
        [XmlText]
        public string Extension { get; set; }

        public override bool IsMatch(FileSystemInfo item, string basePath)
        {
            if (item is FileInfo)
            {
                string thisExtension = Extension.TrimStart('.');
                var extensions = GetPossibleExtensions(item.Name);
                return extensions.Any(e => e.TrimStart('.').Equals(thisExtension, StringComparison.CurrentCultureIgnoreCase));
            }
            return false;
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
    }
}
