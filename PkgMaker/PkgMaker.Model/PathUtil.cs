using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PkgMaker.Core
{
    public static class PathUtil
    {
        public static string GetFullPath(string basePath, string absoluteOrRelativePath)
        {
            return
                Path.IsPathRooted(absoluteOrRelativePath)
                    ? absoluteOrRelativePath
                    : Path.GetFullPath(Path.Combine(basePath, absoluteOrRelativePath));
        }

        public static string GetRelativePath(string basePath, string absolutePath)
        {
            return absolutePath.Substring(basePath.Trim('\\').Length + 1);
        }


    }
}
