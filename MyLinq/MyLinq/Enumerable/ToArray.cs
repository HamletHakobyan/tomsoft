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
            List<TSource> list = source.ToList();
            return list.ToArray();
        }
    }
}
