using System;
using System.Collections;
using System.Collections.Generic;
using Linq = System.Linq;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<Linq.IGrouping<TKey, TSource>> GroupBy<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector, Identity, null);
        }

        public static IEnumerable<Linq.IGrouping<TKey, TSource>> GroupBy<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            return source.GroupBy(keySelector, Identity, comparer);
        }

        public static IEnumerable<Linq.IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector)
        {
            return source.GroupBy(keySelector, elementSelector, null);
        }

        public static IEnumerable<Linq.IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            IEqualityComparer<TKey> comparer)
        {
            source.CheckArgumentNotNull("source");
            keySelector.CheckArgumentNotNull("keySelector");
            elementSelector.CheckArgumentNotNull("elementSelector");

            return source.GroupByImpl(keySelector, elementSelector, comparer);
            /*return new GroupByEnumerable<TSource, TKey, TElement>(
                source,
                keySelector,
                elementSelector,
                comparer);*/
        }

        //private class GroupByEnumerable<TSource, TKey, TElement> : IEnumerable<Linq.IGrouping<TKey, TElement>>
        //{
        //    private readonly IEnumerable<TSource> _source;
        //    private readonly Func<TSource, TKey> _keySelector;
        //    private readonly Func<TSource, TElement> _elementSelector;
        //    private readonly IEqualityComparer<TKey> _comparer;

        //    public GroupByEnumerable(
        //        IEnumerable<TSource> source, 
        //        Func<TSource, TKey> keySelector, 
        //        Func<TSource, TElement> elementSelector, 
        //        IEqualityComparer<TKey> comparer)
        //    {
        //        _source = source;
        //        _keySelector = keySelector;
        //        _elementSelector = elementSelector;
        //        _comparer = comparer;
        //    }

        //    public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        //    {
        //        var lookup = _source.ToLookup(_keySelector, _elementSelector, _comparer);
        //        return GetEnumeratorImpl(lookup);
        //    }

        //    private static IEnumerator<IGrouping<TKey, TElement>> GetEnumeratorImpl(Linq.ILookup<TKey, TElement> lookup)
        //    {
        //        foreach (var grouping in lookup)
        //        {
        //            yield return grouping;
        //        }
        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }
        //}

        private static IEnumerable<Linq.IGrouping<TKey, TElement>> GroupByImpl<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            IEqualityComparer<TKey> comparer)
        {
            var lookup = source.ToLookup(keySelector, elementSelector, comparer);
            foreach (var grouping in lookup)
            {
                yield return grouping;
            }
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            return source.GroupBy(keySelector, Identity, resultSelector, null);
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            return source.GroupBy(keySelector, Identity, resultSelector, comparer);
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            return source.GroupBy(keySelector, elementSelector, resultSelector, null);
        }
         
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            resultSelector.CheckArgumentNotNull("resultSelector");

            return source.GroupBy(keySelector, elementSelector, comparer)
                .Select(g => resultSelector(g.Key, g));
        }
    }
}
