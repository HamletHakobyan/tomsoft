using System.IO;
using System.Xml.Serialization;
namespace PkgMaker.Core
{
    public abstract class ExclusionBase
    {
        public abstract ExclusionTarget Target { get; }

        public virtual bool IsMatch(FileSystemInfo item, string basePath, PackageProperties properties)
        {
            bool isDirectory = (item is DirectoryInfo);
            if (this.Target == ExclusionTarget.File && isDirectory)
                return false;
            if (this.Target == ExclusionTarget.Directory && !isDirectory)
                return false;
            return true;
        }
    }

    public enum ExclusionTarget
    {
        Both,
        File,
        Directory
    }
}
