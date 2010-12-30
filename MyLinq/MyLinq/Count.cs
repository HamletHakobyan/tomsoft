using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            source.CheckArgumentNotNull("source");

            ICollection<TSource> genericCollection = source as ICollection<TSource>;
            if (genericCollection != null)
                return genericCollection.Count;

            ICollection collection = source as ICollection;
            if (collection != null)
                return collection.Count;

            int n = 0;
            checked
            {
#pragma warning disable 168
                foreach (var item in source)
#pragma warning restore 168
                {
                    n++;
                }
            }
            return n;
        }

        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");

            int n = 0;
            checked
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                        n++;
                }
            }
            return n;
        }
    }
}
