using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }
    }
}
