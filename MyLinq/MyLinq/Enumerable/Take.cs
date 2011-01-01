using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            source.CheckArgumentNotNull("source");
            count.CheckArgumentGreaterThanOrEqual("count", 0);
            return TakeImpl(source, count);
        }

        private static IEnumerable<T> TakeImpl<T>(IEnumerable<T> source, int count)
        {
            int n = 0;
            foreach (var item in source)
            {
                if (n > count)
                    yield break;
                yield return item;
                n++;
            }
        }
    }
}
