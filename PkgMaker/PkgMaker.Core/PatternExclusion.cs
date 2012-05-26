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

        public override bool IsMatch(FileSystemInfo item, string basePath, PackageProperties properties)
        {
            if (base.IsMatch(item, basePath, properties))
            {
                string relativePath = PathUtil.GetRelativePath(basePath, item.FullName);
                string pattern = properties.Expand(this.Pattern);
                return Regex.IsMatch(relativePath, pattern, RegexOptions.IgnoreCase);
            }
            return false;
        }
    }
}
