using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TSource Aggregate<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, TSource> func)
        {
            source.CheckArgumentNotNull("source");
            func.CheckArgumentNotNull("func");

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("source contains no elements.");
                TSource acc = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    acc = func(acc, enumerator.Current);
                }
                return acc;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            return source.Aggregate(seed, func, Identity);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector)
        {
            source.CheckArgumentNotNull("source");
            func.CheckArgumentNotNull("func");
            resultSelector.CheckArgumentNotNull("resultSelector");

            TAccumulate acc = seed;
            foreach (var item in source)
            {
                acc = func(acc, item);
            }
            return resultSelector(acc);
        }
    }
}
