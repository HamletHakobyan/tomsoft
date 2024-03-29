﻿using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            source.CheckArgumentNotNull("source");
            return source.TakeImpl(count);
        }

        private static IEnumerable<T> TakeImpl<T>(this IEnumerable<T> source, int count)
        {
            if (count <= 0)
                yield break;

            int n = 0;
            foreach (var item in source)
            {
                if (n >= count)
                    yield break;
                yield return item;
                n++;
            }
        }
    }
}
