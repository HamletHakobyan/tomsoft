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

        [XmlAttribute("Target")]
        public ExclusionTarget ActualTarget { get; set; }

        public override ExclusionTarget Target
        {
            get { return ActualTarget; }
        }

        public override bool IsMatch(FileSystemInfo item, string basePath)
        {
            if (base.IsMatch(item, basePath))
            {
                string relativePath = PathUtil.GetRelativePath(basePath, item.FullName);
                return Regex.IsMatch(relativePath, this.Pattern, RegexOptions.IgnoreCase);
            }
            return false;
        }
    }
}
