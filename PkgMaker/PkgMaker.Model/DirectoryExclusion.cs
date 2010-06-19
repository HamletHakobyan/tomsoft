using System.Diagnostics;
using System.Xml.Serialization;

namespace PkgMaker.Model
{
    [DebuggerDisplay("DirectoryExclusion [{Path}]")]
    public class DirectoryExclusion : FileSystemEntryExclusion
    {
        public override bool Directory
        {
            get { return true; }
        }
    }
}
