using System;
using System.Collections.Generic;
using Linq = System.Linq;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.OrderByCore(keySelector, null, false);
        }

        public static Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.OrderByCore(keySelector, comparer, false);
        }

        public static Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.OrderByCore(keySelector, null, true);
        }

        public static Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.OrderByCore(keySelector, comparer, true);
        }

        private static Linq.IOrderedEnumerable<TSource> OrderByCore<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            source.CheckArgumentNotNull("source");
            keySelector.CheckArgumentNotNull("keySelector");
            
            var elementComparer = OrderedEnumerable<TSource>.CreateComparer<TKey>(keySelector, comparer, descending);

            return new OrderedEnumerable<TSource>(source, elementComparer);
        }
    }
}
