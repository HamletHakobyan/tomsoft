using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            count.CheckArgumentGreaterThanOrEqual("count", 0);
            return RepeatImpl(element, count);
        }

        private static IEnumerable<TResult> RepeatImpl<TResult>(TResult element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return element;
            }
        }
    }
}
