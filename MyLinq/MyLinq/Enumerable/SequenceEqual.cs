using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other)
        {
            return source.SequenceEqual(other, EqualityComparer<TSource>.Default);
        }

        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other, IEqualityComparer<TSource> comparer)
        {
            source.CheckArgumentNotNull("source");
            other.CheckArgumentNotNull("other");
            comparer.CheckArgumentNotNull("comparer");

            using (IEnumerator<TSource> sourceEnumerator = source.GetEnumerator())
            using (IEnumerator<TSource> otherEnumerator = source.GetEnumerator())
            {
                bool sourceNext = sourceEnumerator.MoveNext();
                bool otherNext = otherEnumerator.MoveNext();
                if (sourceNext != otherNext)
                    return false;

                if (sourceNext)
                {
                    if (!comparer.Equals(sourceEnumerator.Current, otherEnumerator.Current))
                        return false;
                }

                return true;
            }
        }
    }
}
