using System;
using System.Reflection;

namespace Mediatek.Helpers
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
