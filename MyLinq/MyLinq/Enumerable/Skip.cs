using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Skip<TSource>(
            this IEnumerable<TSource> source,
            int count)
        {
            source.CheckArgumentNotNull("source");
            return source.SkipImpl(count);
        }

        private static IEnumerable<TSource> SkipImpl<TSource>(
            this IEnumerable<TSource> source,
            int count)
        {
            int n = -1;
            foreach (var item in source)
            {
                n++;
                if (n < count)
                    continue;
                yield return item;
            }
        }
    }
}
