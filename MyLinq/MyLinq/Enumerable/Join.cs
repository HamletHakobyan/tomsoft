using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, TInner, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            outer.CheckArgumentNotNull("outer");
            inner.CheckArgumentNotNull("inner");
            outerKeySelector.CheckArgumentNotNull("outerKeySelector");
            innerKeySelector.CheckArgumentNotNull("innerKeySelector");
            resultSelector.CheckArgumentNotNull("resultSelector");

            return outer.JoinImpl(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        private static IEnumerable<TResult> JoinImpl<TOuter, TInner, TKey, TResult>( 
            this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, 
            Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, TInner, TResult> resultSelector, 
            IEqualityComparer<TKey> comparer)
        {
            comparer = comparer ?? EqualityComparer<TKey>.Default;

            var innerLookup = inner.ToLookup(innerKeySelector, comparer);

            foreach (var outerItem in outer)
            {
                var key = outerKeySelector(outerItem);
                foreach (var innerItem in innerLookup[key])
                {
                    yield return resultSelector(outerItem, innerItem);
                }
            }
        }
    }
}
