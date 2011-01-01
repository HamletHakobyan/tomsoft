using System;
using System.Collections.Generic;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return first.Union(second, null);
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            first.CheckArgumentNotNull("first");
            second.CheckArgumentNotNull("second");

            return first.UnionImpl(second, comparer);
        }

        private static IEnumerable<TSource> UnionImpl<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;

            HashSet<TSource> seen = new HashSet<TSource>(comparer);
            foreach (var item in first)
            {
                if (seen.Add(item))
                    yield return item;
            }
            foreach (var item in second)
            {
                if (seen.Add(item))
                    yield return item;
            }
        }
    }
}
