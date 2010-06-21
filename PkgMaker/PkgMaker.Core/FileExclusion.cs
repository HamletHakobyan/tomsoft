using System.Diagnostics;

namespace PkgMaker.Core
{
    [DebuggerDisplay("FileExclusion [{Path}]")]
    public class FileExclusion : FileSystemEntryExclusion
    {
        public override ExclusionTarget Target
        {
            get { return ExclusionTarget.File; }
        }
    }
}
