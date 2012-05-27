using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PkgMaker.Core
{
    [DebuggerDisplay("PatternFilter [{Pattern}]")]
    public class PatternFilter : FilterBase
    {
        [XmlText]
        public string Pattern { get; set; }

        [XmlAttribute("Target")]
        public FilterTarget ActualTarget { get; set; }

        public override FilterTarget Target
        {
            get { return ActualTarget; }
        }

        protected override bool IsMatchCore(FileSystemInfo item, string basePath)
        {
            if (base.IsMatchCore(item, basePath))
            {
                string relativePath = PathUtil.GetRelativePath(basePath, item.FullName);
                return _regex.IsMatch(relativePath);
            }
            return false;
        }

        private Regex _regex;

        public override void Prepare(PackageProperties properties)
        {
            base.Prepare(properties);
            string pattern = properties.Expand(this.Pattern, isRegex: true);
            _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return string.Format("PatternFilter [{0}]", Pattern);
        }
    }
}
