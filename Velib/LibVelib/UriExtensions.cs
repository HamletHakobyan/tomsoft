using System;
using System.IO;

namespace Velib
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri baseUri, string relativeUri)
        {
            string fullUri = Path.Combine(baseUri.AbsoluteUri, relativeUri);
            return new Uri(fullUri);
        }
    }
}
