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
            int count;
            TElement[] data = _source.ToBuffer(out count);
            MergeSort(data, count);
            for (int i = 0; i < count; i++)
            {
                yield return data[i];
            }
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

        private void MergeSort(TElement[] data, int count)
        {
            TElement[] tmp = new TElement[count];
            MergeSort(data, tmp, 0, count);
        }

        private void MergeSort(TElement[] data, TElement[] tempBuffer, int start, int count)
        {
            if (count < 2)
            {
                return;
            }
            
            if (count == 2)
            {
                TElement x = data[start];
                TElement y = data[start + 1];
                if (_comparer.Compare(x, y) > 0)
                {
                    data[start] = y;
                    data[start + 1] = x;
                }
                return;
            }

            int leftCount = count / 2;
            int rightCount = count - leftCount;
            MergeSort(data, tempBuffer, start, leftCount);
            MergeSort(data, tempBuffer, start + leftCount, rightCount);
            Merge(data, tempBuffer, start, start + leftCount, count);
        }

        private void Merge(TElement[] data, TElement[] tempBuffer, int start, int middle, int count)
        {
            int index = 0;
            int leftIndex = start;
            int rightIndex = middle;

            while (leftIndex < middle && rightIndex < count)
            {
                TElement left = data[leftIndex];
                TElement right = data[rightIndex];
                if (_comparer.Compare(left, right) <= 0)
                {
                    tempBuffer[index] = left;
                    leftIndex++;
                }
                else
                {
                    tempBuffer[index] = right;
                    rightIndex++;
                }
                index++;
            }
            while (leftIndex < middle)
            {
                tempBuffer[index++] = data[leftIndex++];
            }
            while (rightIndex < count)
            {
                tempBuffer[index++] = data[rightIndex++];
            }
            Array.Copy(tempBuffer, 0, data, start, count);
        }
    }
}
