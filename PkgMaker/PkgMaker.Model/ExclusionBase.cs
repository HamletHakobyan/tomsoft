using System.IO;
namespace PkgMaker.Core
{
    public abstract class ExclusionBase
    {
        public abstract bool IsMatch(FileSystemInfo item, string basePath);
    }
}
