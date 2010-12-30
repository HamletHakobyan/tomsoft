using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            TAccumulate acc = seed;
            foreach (var item in source)
            {
                acc = func(acc, item);
            }
            return acc;
        }
    }
}
