﻿using System;
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

        public abstract bool Directory { get; }

        public override bool IsMatch(FileSystemInfo item, string basePath)
        {
            if (this.Directory == (item is DirectoryInfo))
            {
                string relativePath = this.Any ? item.Name : PathUtil.GetRelativePath(basePath, item.FullName);
                return relativePath.Equals(this.Path.TrimEnd('\\'), StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }
    }
}