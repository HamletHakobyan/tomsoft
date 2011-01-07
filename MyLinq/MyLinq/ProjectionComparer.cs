using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    class ProjectionComparer<TElement, TKey> : IComparer<TElement>
    {
        private readonly Func<TElement, TKey> _keySelector;
        private readonly IComparer<TKey> _keyComparer;

        public ProjectionComparer(Func<TElement, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            _keySelector = Memoize(keySelector);
            _keyComparer = keyComparer ?? Comparer<TKey>.Default;
        }

        public int Compare(TElement x, TElement y)
        {
            return _keyComparer.Compare(_keySelector(x), _keySelector(y));
        }

        private static Func<TElement, TKey> Memoize(Func<TElement, TKey> keySelector)
        {
            var cache = new Dictionary<TElement, TKey>();
            return element =>
                       {
                           TKey comparisonKey;
                           if (!cache.TryGetValue(element, out comparisonKey))
                           {
                               comparisonKey = keySelector(element);
                               cache[element] = comparisonKey;
                           }
                           return comparisonKey;
                       };
        }
    }
}
