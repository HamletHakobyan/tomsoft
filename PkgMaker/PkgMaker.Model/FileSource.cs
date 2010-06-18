using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PkgMaker.Model
{
    [DebuggerDisplay("FileSource [{Path}]")]
    public class FileSource : ContentSourceBase
    {
        [XmlAttribute]
        public string Path { get; set; }
    }
}
