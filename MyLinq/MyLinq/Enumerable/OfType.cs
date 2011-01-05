using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            source.CheckArgumentNotNull("source");
            return source.OfTypeImpl<TResult>();
        }

        private static IEnumerable<TResult> OfTypeImpl<TResult>(this IEnumerable source)
        {
            foreach (object item in source)
            {
                if (item is TResult)
                    yield return (TResult)item;
            }
        }
    }
}
