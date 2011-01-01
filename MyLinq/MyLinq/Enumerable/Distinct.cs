using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            source.CheckArgumentNotNull("source");
            return source.DistinctImpl(comparer);
        }

        private static IEnumerable<TSource> DistinctImpl<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            HashSet<TSource> seen = new HashSet<TSource>(comparer);
            foreach (var item in source)
            {
                if (seen.Add(item))
                    yield return item;
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            return source.Distinct(null);
        }
    }
}
