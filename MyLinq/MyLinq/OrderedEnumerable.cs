using System;
using System.Collections;
using System.Collections.Generic;
using Linq = System.Linq;

namespace MyLinq
{
    class OrderedEnumerable<TElement> : Linq.IOrderedEnumerable<TElement>
    {
        private readonly IEnumerable<TElement> _source;
        private readonly IComparer<TElement> _comparer;

        public OrderedEnumerable(IEnumerable<TElement> source, IComparer<TElement> comparer)
        {
            _source = source;
            _comparer = comparer;
        }

        public Linq.IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            keySelector.CheckArgumentNotNull("keySelector");

            var secondComparer = CreateComparer(keySelector, comparer, descending);
            var compoundComparer = new CompoundComparer<TElement>(_comparer, secondComparer);
            return new OrderedEnumerable<TElement>(_source, compoundComparer);
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            // TODO: to be improved
            var list = _source.Select((x, i) => new IndexedElement(x, i)).ToList();
            var elementComparer = new ProjectionComparer<IndexedElement, TElement>(i => i.Element, _comparer);
            var indexComparer = new ProjectionComparer<IndexedElement, int>(i => i.Index, null);
            var actualComparer = new CompoundComparer<IndexedElement>(elementComparer, indexComparer);
            list.Sort(actualComparer);
            return list.Select(i => i.Element).GetEnumerator();
        }

        private class IndexedElement
        {
            private readonly TElement _element;
            private readonly int _index;

            public IndexedElement(TElement element, int index)
            {
                _element = element;
                _index = index;
            }

            public int Index { get { return _index; } }
            public TElement Element { get { return _element; } }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal static IComparer<TElement> CreateComparer<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            if (descending)
                keyComparer = new ReverseComparer<TKey>(keyComparer);
            return new ProjectionComparer<TElement, TKey>(keySelector, keyComparer);
        }
    }
}
