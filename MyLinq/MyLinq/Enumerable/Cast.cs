using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            source.CheckArgumentNotNull("source");
            return source.CastImpl<TResult>();
        }

        private static IEnumerable<TResult> CastImpl<TResult>(this IEnumerable source)
        {
            foreach (var item in source)
            {
                yield return (TResult)item;
            }
        }
    }
}
