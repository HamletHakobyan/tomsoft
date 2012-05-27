using System.Diagnostics;

namespace PkgMaker.Core
{
    [DebuggerDisplay("FileFilter [{Path}]")]
    public class FileFilter : FileSystemEntryFilter
    {
        public override FilterTarget Target
        {
            get { return FilterTarget.File; }
        }

        public override string ToString()
        {
            return string.Format("FileFilter [{0}'{1}']", Any ? "Any " : string.Empty, Path);
        }
    }
}
