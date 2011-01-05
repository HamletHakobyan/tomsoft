using System;
using System.Collections.Generic;
using Linq = System.Linq;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static Linq.IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this Linq.IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.ThenByCore(keySelector, null, false);
        }

        public static Linq.IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this Linq.IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.ThenByCore(keySelector, comparer, false);
        }

        public static Linq.IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this Linq.IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.ThenByCore(keySelector, null, true);
        }

        public static Linq.IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this Linq.IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.ThenByCore(keySelector, comparer, true);
        }

        private static Linq.IOrderedEnumerable<TSource> ThenByCore<TSource, TKey>(
            this Linq.IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, descending);
        }
    }
}
