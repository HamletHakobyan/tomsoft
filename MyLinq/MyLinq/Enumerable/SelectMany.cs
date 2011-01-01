using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            return source.SelectManyImpl(selector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (var item in source)
            {
                foreach (var subItem in selector(item))
                {
                    yield return subItem;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TResult>> selector)
        {
            source.CheckArgumentNotNull("source");
            selector.CheckArgumentNotNull("selector");
            return source.SelectManyImpl(selector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TResult>> selector)
        {
            int n = 0;
            foreach (var item in source)
            {
                foreach (var subItem in selector(item, n))
                {
                    yield return subItem;
                }
                n++;
            }
        }
        
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            source.CheckArgumentNotNull("source");
            collectionSelector.CheckArgumentNotNull("collectionSelector");
            resultSelector.CheckArgumentNotNull("resultSelector");
            return source.SelectManyImpl(collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (var item in source)
            {
                foreach (var subItem in collectionSelector(item))
                {
                    yield return resultSelector(item, subItem);
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            source.CheckArgumentNotNull("source");
            collectionSelector.CheckArgumentNotNull("collectionSelector");
            resultSelector.CheckArgumentNotNull("resultSelector");
            return source.SelectManyImpl(collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            int n = 0;
            foreach (var item in source)
            {
                foreach (var subItem in collectionSelector(item, n))
                {
                    yield return resultSelector(item, subItem);
                }
                n++;
            }
        }
    }
}
