using System.Diagnostics;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("DirectoryFilter [{Path}]")]
    public class DirectoryFilter : FileSystemEntryFilter
    {
        public override FilterTarget Target
        {
            get { return FilterTarget.Directory; }
        }

        public override string ToString()
        {
            return string.Format("DirectoryFilter [{0}'{1}']", Any ? "Any " : string.Empty, Path);
        }
    }
}
