using System;
using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            source.CheckArgumentNotNull("source");

            ICollection<TSource> collection = source as ICollection<TSource>;
            if (collection != null)
            {
                TSource[] array = new TSource[collection.Count];
                collection.CopyTo(array, 0);
                return array;
            }

            int count;
            TSource[] buffer = source.ToBuffer(out count);
            TrimArray(ref buffer, count);
            return buffer;
        }

        internal static TSource[] ToBuffer<TSource>(this IEnumerable<TSource> source, out int count)
        {
            int index = 0;
            TSource[] buffer = new TSource[16];
            foreach (var item in source)
            {
                if (index == buffer.Length)
                {
                    Array.Resize(ref buffer, buffer.Length * 2);
                }
                buffer[index++] = item;
            }
            count = index;
            return buffer;
        }

        private static void TrimArray<T>(ref T[] array, int count)
        {
            if (array.Length > count)
            {
                Array.Resize(ref array, count);
            }
        }
    }
}
