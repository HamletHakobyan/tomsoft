using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> TakeWhile<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            return source.TakeWhileImpl(predicate);
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                    yield break;
                yield return item;
            }
        }

        public static IEnumerable<TSource> TakeWhile<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            return source.TakeWhileImpl(predicate);
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            int n = 0;
            foreach (var item in source)
            {
                if (!predicate(item, n))
                    yield break;
                yield return item;
                n++;
            }
        }
    }
}
