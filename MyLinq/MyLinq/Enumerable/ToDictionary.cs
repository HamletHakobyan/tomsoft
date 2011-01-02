using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector)
        {
            return source.ToDictionary(keySelector, Identity, null);
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector)
        {
            return source.ToDictionary(keySelector, elementSelector, null);
        }

        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary(keySelector, Identity, comparer);
        }
         
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            IEqualityComparer<TKey> comparer)
        {
            source.CheckArgumentNotNull("source");
            keySelector.CheckArgumentNotNull("keySelector");
            elementSelector.CheckArgumentNotNull("elementSelector");

            var dictionary = new Dictionary<TKey, TElement>(comparer);
            foreach (var item in source)
            {
                dictionary.Add(keySelector(item), elementSelector(item));
            }
            return dictionary;
        }
    }
}
