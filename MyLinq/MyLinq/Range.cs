using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<int> Range(int start, int count)
        {
            count.CheckArgumentGreaterThanOrEqual("count", 0);
            if (start + count - 1L > int.MaxValue)
                throw new ArgumentOutOfRangeException("count", "start + count - 1 must be less than or equal to Int32.MaxValue");
            return RangeImpl(start, count);
        }

        private static IEnumerable<int> RangeImpl(int start, int count)
        {
            for (int i = start; i < count; i++)
            {
                yield return i;
            }
        }
    }
}
