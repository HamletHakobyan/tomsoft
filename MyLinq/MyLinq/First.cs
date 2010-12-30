using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            return source.FirstImpl(true);
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.FirstImpl(predicate, true);
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.FirstImpl(false);
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.FirstImpl(predicate, false);
        }

        private static TSource FirstImpl<TSource>(this IEnumerable<TSource> source, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    return enumerator.Current;
                if (throwIfNone)
                    throw new InvalidOperationException("The source sequence is empty.");
                return default(TSource);
            }
        }

        private static TSource FirstImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            foreach (var item in source)
            {
                if (predicate(item))
                    return item;
            }
            if (throwIfNone)
                throw new InvalidOperationException("No element satisfies the condition in predicate.");
            return default(TSource);
        }
    }
}
