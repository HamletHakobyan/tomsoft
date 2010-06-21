using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("PatternExclusion [{Pattern}]")]
    public class PatternExclusion : ExclusionBase
    {
        [XmlText]
        public string Pattern { get; set; }

        public override bool IsMatch(FileSystemInfo item, string basePath)
        {
            string relativePath = PathUtil.GetRelativePath(basePath, item.FullName);
            return Regex.IsMatch(relativePath, this.Pattern, RegexOptions.IgnoreCase);
        }
    }
}
