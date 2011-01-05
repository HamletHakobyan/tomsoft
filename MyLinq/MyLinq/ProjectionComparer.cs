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
            _keySelector = keySelector;
            _keyComparer = keyComparer ?? Comparer<TKey>.Default;
        }

        public int Compare(TElement x, TElement y)
        {
            return _keyComparer.Compare(_keySelector(x), _keySelector(y));
        }
    }
}
