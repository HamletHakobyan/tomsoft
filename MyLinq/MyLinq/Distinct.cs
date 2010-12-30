using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            source.CheckArgumentNotNull("source");
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            Dictionary<TSource, bool> seen = new Dictionary<TSource,bool>(comparer);
            foreach (var item in source)
            {
                if (seen.ContainsKey(item)) continue;
                seen.Add(item, false);
                yield return item;
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            return source.Distinct(null);
        }
    }
}
