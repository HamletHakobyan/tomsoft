using System;
using System.IO;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    public abstract class FileSystemEntryExclusion : ExclusionBase
    {
        [XmlText]
        public string Path { get; set; }
        
        [XmlAttribute]
        public bool Any { get; set; }

        public override bool IsMatch(FileSystemInfo item, string basePath, PackageProperties properties)
        {
            if (base.IsMatch(item, basePath, properties))
            {
                string relativePath = this.Any ? item.Name : PathUtil.GetRelativePath(basePath, item.FullName);
                string path = properties.Expand(this.Path);
                return relativePath.Equals(path.TrimEnd('\\'), StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }
    }
}
