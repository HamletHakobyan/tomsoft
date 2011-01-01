using System;
using System.Collections.Generic;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
        {
            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            outer.CheckArgumentNotNull("outer");
            inner.CheckArgumentNotNull("inner");
            outerKeySelector.CheckArgumentNotNull("outerKeySelector");
            innerKeySelector.CheckArgumentNotNull("innerKeySelector");
            resultSelector.CheckArgumentNotNull("resultSelector");

            return outer.GroupJoinImpl(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        private static IEnumerable<TResult> GroupJoinImpl<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            comparer = comparer ?? EqualityComparer<TKey>.Default;

            var innerLookup = inner.ToLookup(innerKeySelector, comparer);

            foreach (var outerItem in outer)
            {
                var key = outerKeySelector(outerItem);
                yield return resultSelector(outerItem, innerLookup[key]);
            }
        }
    }
}
