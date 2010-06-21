﻿using System.Diagnostics;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("DirectoryExclusion [{Path}]")]
    public class DirectoryExclusion : FileSystemEntryExclusion
    {
        public override ExclusionTarget Target
        {
            get { return ExclusionTarget.Directory; }
        }
    }
}
