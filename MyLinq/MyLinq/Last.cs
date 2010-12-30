using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            return source.LastImpl(true);
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.LastImpl(predicate, true);
        }

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.LastImpl(false);
        }

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.LastImpl(predicate, false);
        }

        private static TSource LastImpl<TSource>(this IEnumerable<TSource> source, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");

            TSource last = default(TSource);
            bool found = false;

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    last = list[list.Count - 1];
                    found = true;
                }
            }
            else
            {
                foreach (var item in source)
                {
                    last = item;
                    found = true;
                }
            }
            if (found)
                return last;
            if (throwIfNone)
                throw new InvalidOperationException("The source sequence is empty.");
            return default(TSource);
        }

        private static TSource LastImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool throwIfNone)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            bool found = false;
            TSource last = default(TSource);
            foreach (var item in source)
            {
                if (!predicate(item))
                    continue;
                last = item;
                found = true;
            }
            if (found)
                return last;
            if (throwIfNone)
                throw new InvalidOperationException("No element satisfies the condition in predicate.");
            return default(TSource);
        }
    }
}
