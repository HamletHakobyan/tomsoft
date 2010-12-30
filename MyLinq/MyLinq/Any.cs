using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            foreach (var item in source)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            source.CheckArgumentNotNull("source");
            using (var enumerator = source.GetEnumerator())
            {
                return enumerator.MoveNext();
            }
        }
    }
}
