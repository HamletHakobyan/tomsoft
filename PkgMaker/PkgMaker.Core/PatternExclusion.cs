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

        [XmlAttribute]
        public ExclusionTarget Target { get; set; }

        public override bool IsMatch(FileSystemInfo item, string basePath)
        {
            bool isDirectory = item is DirectoryInfo;
            if (Target == ExclusionTarget.File && isDirectory)
                return false;
            if (Target == ExclusionTarget.Directory && !isDirectory)
                return false;

            string relativePath = PathUtil.GetRelativePath(basePath, item.FullName);
            return Regex.IsMatch(relativePath, this.Pattern, RegexOptions.IgnoreCase);
        }
    }

    public enum ExclusionTarget
    {
        Both,
        File,
        Directory
    }
}
