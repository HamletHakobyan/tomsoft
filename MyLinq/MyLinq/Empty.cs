using System.Collections.Generic;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyContainer<TResult>.EmptyArray;
        }

        private static class EmptyContainer<TResult>
        {
            public static readonly TResult[] EmptyArray = new TResult[0];
        }
    }
}
