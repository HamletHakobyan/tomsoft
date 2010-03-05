using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Mediatek.Web.Helpers
{
    public static class HttpRequestExtensions
    {
        public static T Get<T>(this HttpRequestBase request, string key)
        {
            return request.Get<T>(key, default(T));
        }

        public static T Get<T>(this HttpRequestBase request, string key, T defaultValue)
        {
            string value = request[key];
            if (value != null)
            {
                return Convert<T>.From(value);
            }
            return defaultValue;
        }

        private static class Convert<T>
        {
            private static TypeConverter _converter = TypeDescriptor.GetConverter(typeof(T));

            public static T From(object value)
            {
                return (T)_converter.ConvertFrom(value);
            }
        }
    }
}