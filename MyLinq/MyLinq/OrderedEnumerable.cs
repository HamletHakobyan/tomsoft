using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        private readonly IEnumerable<TElement> _source;

        public OrderedEnumerable(IEnumerable<TElement> source)
        {
            _source = source;
        }

        public virtual IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedEnumerable<TElement, TKey>(_source, keySelector, comparer, descending);
        }

        public virtual IEnumerator<TElement> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected IEnumerable<TElement> Source
        {
            get { return _source; }
        }
    }

    class OrderedEnumerable<TElement, TFirstKey> : OrderedEnumerable<TElement>
    {
        private readonly IComparer<TElement> _comparer;
        private readonly Func<TElement, TFirstKey> _keySelector;
        private readonly IComparer<TFirstKey> _keyComparer;

        public OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TFirstKey> keySelector, IComparer<TFirstKey> keyComparer, bool descending)
            : base(source)
        {
            _comparer = CreateComparer(keySelector, keyComparer, descending);
        }

        private static IComparer<TElement> CreateComparer<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            if (descending)
                keyComparer = new ReverseComparer<TKey>(keyComparer);
            return new ProjectionComparer<TElement, TKey>(keySelector, keyComparer);
        }

        public override IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            //TODO
            throw new NotImplementedException();
            return base.CreateOrderedEnumerable<TKey>(keySelector, comparer, descending);
        }

        public override IEnumerator<TElement> GetEnumerator()
        {
            var list = Source.ToList();
            list.Sort(_comparer);
            return list.GetEnumerator();
        }
    }
}
