using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            source.CheckArgumentNotNull("source");
            return new List<TSource>(source);
        }
    }
}
