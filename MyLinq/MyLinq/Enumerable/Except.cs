using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            return first.Except(second, null);
        }

        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            first.CheckArgumentNotNull("first");
            second.CheckArgumentNotNull("second");

            return first.ExceptImpl(second, comparer);
        }

        private static IEnumerable<TSource> ExceptImpl<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            HashSet<TSource> secondSet = new HashSet<TSource>(second, comparer);
            foreach (var item in first)
            {
                if (secondSet.Add(item))
                    yield return item;
            }
        }
    }
}
