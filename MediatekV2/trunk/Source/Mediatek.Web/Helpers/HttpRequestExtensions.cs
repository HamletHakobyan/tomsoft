using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Mediatek.Web.Helpers
{
    public static class HttpRequestExtensions
    {
        public static T Get<T>(this HttpRequestBase request, string key)
        {
            return Get(request, key, default(T));
        }

        public static T Get<T>(this HttpRequestBase request, string key, T defaultValue)
        {
            string value = request[key];
            return value != null
                ? Convert<T>.From(value)
                : defaultValue;
        }

        private static class Convert<T>
        {
            private static readonly TypeConverter _converter = TypeDescriptor.GetConverter(typeof(T));

            public static T From(object value)
            {
                return (T)_converter.ConvertFrom(value);
            }
        }
    }
}