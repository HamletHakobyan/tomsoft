using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            source.CheckArgumentNotNull("source");
            return source.DefaultIfEmptyImpl(defaultValue);
        }

        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty(default(TSource));
        }

        private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                }
                else
                {
                    yield return defaultValue;
                }
            }
        }
    }
}
