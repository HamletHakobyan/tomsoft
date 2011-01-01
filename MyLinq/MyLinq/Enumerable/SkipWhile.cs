using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> SkipWhile<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            return source.SkipWhileImpl(predicate);
        }

        private static IEnumerable<TSource> SkipWhileImpl<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            //bool stopSkipping = false;
            //foreach (var item in source)
            //{
            //    if (!stopSkipping)
            //    {
            //        if (predicate(item))
            //        {
            //            continue;
            //        }
            //        stopSkipping = true;
            //    }
            //    yield return item;
            //}

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (!predicate(enumerator.Current))
                    {
                        yield return enumerator.Current;
                        break;
                    }
                }

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(
    this IEnumerable<TSource> source,
    Func<TSource, int, bool> predicate)
        {
            source.CheckArgumentNotNull("source");
            predicate.CheckArgumentNotNull("predicate");
            return source.SkipWhileImpl(predicate);
        }

        private static IEnumerable<TSource> SkipWhileImpl<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            //int n = -1;
            //bool stopSkipping = false;
            //foreach (var item in source)
            //{
            //    n++;
            //    if (!stopSkipping)
            //    {
            //        if (predicate(item, n))
            //        {
            //            continue;
            //        }
            //        stopSkipping = true;
            //    }
            //    yield return item;
            //}

            int n = -1;
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    n++;
                    if (!predicate(enumerator.Current, n))
                    {
                        yield return enumerator.Current;
                        break;
                    }
                }

                while (enumerator.MoveNext())
                {
                    n++;
                    yield return enumerator.Current;
                }
            }
        }

    }
}
