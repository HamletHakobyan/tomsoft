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
                string extension = ((FileInfo)item).Extension.TrimStart('.');
                return extension.Equals(this.Extension.TrimStart('.'), StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }
    }
}
