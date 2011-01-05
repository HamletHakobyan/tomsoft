using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLinq
{
    class CompoundComparer<TElement> : IComparer<TElement>
    {
        private readonly IComparer<TElement> _firstComparer;
        private readonly IComparer<TElement> _secondComparer;

        public CompoundComparer(IComparer<TElement> firstComparer, IComparer<TElement> secondComparer)
        {
            _firstComparer = firstComparer;
            _secondComparer = secondComparer;
        }

        public int Compare(TElement x, TElement y)
        {
            int result = _firstComparer.Compare(x, y);
            if (result == 0)
                result = _secondComparer.Compare(x, y);
            return result;
        }
    }
}
