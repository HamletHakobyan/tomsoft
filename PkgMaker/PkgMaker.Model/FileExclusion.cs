using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
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
