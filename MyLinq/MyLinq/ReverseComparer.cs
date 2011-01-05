using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    class ReverseComparer<TElement> : IComparer<TElement>
    {
        private readonly IComparer<TElement> _baseComparer;

        public ReverseComparer(IComparer<TElement> baseComparer)
        {
            _baseComparer = baseComparer ?? Comparer<TElement>.Default;
        }

        public int Compare(TElement x, TElement y)
        {
            return _baseComparer.Compare(y, x);
        }
    }
}
