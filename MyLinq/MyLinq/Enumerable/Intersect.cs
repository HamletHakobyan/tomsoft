using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Intersect<TSource>( 
            this IEnumerable<TSource> first, 
            IEnumerable<TSource> second)
        {
            return first.Intersect(second, null);
        }

        public static IEnumerable<TSource> Intersect<TSource>( 
            this IEnumerable<TSource> first, 
            IEnumerable<TSource> second, 
            IEqualityComparer<TSource> comparer)
        {
            first.CheckArgumentNotNull("first");
            second.CheckArgumentNotNull("second");

            return first.IntersectImpl(second, comparer);
        }

        private static IEnumerable<TSource> IntersectImpl<TSource>( 
            this IEnumerable<TSource> first, 
            IEnumerable<TSource> second, 
            IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            HashSet<TSource> secondSet = new HashSet<TSource>(second, comparer);
            foreach (var item in first)
            {
                if (secondSet.Remove(item))
                    yield return item;
            }
        }
    }
}
