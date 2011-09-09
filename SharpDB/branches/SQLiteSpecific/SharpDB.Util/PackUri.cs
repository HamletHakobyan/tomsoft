using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SharpDB.Util
{
    public static class PackUri
    {
        public static Uri MakePackUri(string relativePath)
        {
            string uriString =
                string.Format(
                    "pack://application:,,,/{0}",
                    relativePath);
            return new Uri(uriString);
        }

        private static Uri MakePackUri(Assembly assembly, string relativePath)
        {
            string uriString =
                string.Format(
                    "pack://application:,,,/{0};component/{1}",
                    assembly.GetName().Name,
                    relativePath);
            return new Uri(uriString);
        }
    }
}
