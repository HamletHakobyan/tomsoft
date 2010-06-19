using System.Diagnostics;

namespace PkgMaker.Core
{
    [DebuggerDisplay("FileExclusion [{Path}]")]
    public class FileExclusion : FileSystemEntryExclusion
    {
        public override bool Directory
        {
            get { return false; }
        }
    }
}
