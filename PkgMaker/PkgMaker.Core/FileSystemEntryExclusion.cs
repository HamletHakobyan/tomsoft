using System;
using System.IO;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    public abstract class FileSystemEntryFilter : FilterBase
    {
        [XmlText]
        public string Path { get; set; }
        
        [XmlAttribute]
        public bool Any { get; set; }

        protected override bool IsMatchCore(FileSystemInfo item, string basePath)
        {
            if (base.IsMatchCore(item, basePath))
            {
                string relativePath = this.Any ? item.Name : PathUtil.GetRelativePath(basePath, item.FullName);
                return relativePath.Equals(_preparedPath, StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }

        private string _preparedPath;
        public override void Prepare(PackageProperties properties)
        {
            base.Prepare(properties);
            _preparedPath = properties.Expand(this.Path).TrimEnd('\\');
        }
    }
}
