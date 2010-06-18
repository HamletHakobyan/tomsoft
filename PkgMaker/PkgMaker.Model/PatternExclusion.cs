using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("PatternExclusion [{Pattern}]")]
    public class PatternExclusion : ExclusionBase
    {
        [XmlText]
        public string Pattern { get; set; }
    }
}
