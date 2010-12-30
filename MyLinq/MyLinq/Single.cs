using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            return source.SingleImpl(true);
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.SingleImpl(predicate, true);
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.SingleImpl(false);
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.SingleImpl(predicate, false);
        }

        private static TSource SingleImpl<TSource>(this IEnumerable<TSource> source, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (throwIfNone)
                        throw new InvalidOperationException("The source sequence is empty.");
                    return default(TSource);
                }
                TSource single = enumerator.Current;
                if (enumerator.MoveNext())
                    throw new InvalidOperationException("The input sequence contains more than one element.");
                return single;
            }
        }

        private static TSource SingleImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            bool found = false;
            TSource single = default(TSource);
            foreach (var item in source)
            {
                if (!predicate(item))
                    continue;

                if (found)
                    throw new InvalidOperationException("More than one element satisfies the condition in predicate.");
                single = item;
                found = true;
            }
            if (found)
                return single;

            if (throwIfNone)
                throw new InvalidOperationException("No element satisfies the condition in predicate.");
            return default(TSource);
        }
    }
}
