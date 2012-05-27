using System;
using System.Diagnostics;
using System.IO;

namespace PkgMaker.Core
{
    public abstract class FilterBase
    {
        public abstract FilterTarget Target { get; }

        public bool IsMatch(FileSystemInfo item, string basePath, PackageProperties properties)
        {
            return IsMatchCore(item, basePath);
        }

        protected virtual bool IsMatchCore(FileSystemInfo item, string basePath)
        {
            if (!_prepared)
                throw new InvalidOperationException("Filter has not been prepared.");

            bool isDirectory = (item is DirectoryInfo);
            if (this.Target == FilterTarget.File && isDirectory)
                return false;
            if (this.Target == FilterTarget.Directory && !isDirectory)
                return false;
            return true;
        }

        private bool _prepared;
        public virtual void Prepare(PackageProperties properties)
        {
            Debug.Print("Preparing {0}", this);
            _prepared = true;
        }
    }

    public enum FilterTarget
    {
        Both,
        File,
        Directory
    }
}