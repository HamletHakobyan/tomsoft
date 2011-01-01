using System;
using System.Collections;
using System.Collections.Generic;
using Linq = System.Linq;

namespace MyLinq
{
    public static partial class Enumerable
    {
        public static Linq.ILookup<TKey, TSource> ToLookup<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector)
        {
            return source.ToLookup(keySelector, x => x, null);
        }

        public static Linq.ILookup<TKey, TSource> ToLookup<TSource, TKey>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            return source.ToLookup(keySelector, x => x, comparer);
        }

        public static Linq.ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector)
        {
            return source.ToLookup(keySelector, elementSelector, null);
        }

        public static Linq.ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>( 
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TElement> elementSelector, 
            IEqualityComparer<TKey> comparer)
        {
            source.CheckArgumentNotNull("source");
            keySelector.CheckArgumentNotNull("keySelector");
            elementSelector.CheckArgumentNotNull("elementSelector");

            comparer = comparer ?? EqualityComparer<TKey>.Default;

            MyLookup<TKey, TElement> lookup = new MyLookup<TKey, TElement>(comparer);
            foreach (var item in source)
            {
                lookup.Add(keySelector(item), elementSelector(item));
            }
            return lookup;
        }

        private class MyLookup<TKey, TElement> : Linq.ILookup<TKey, TElement>
        {
            private readonly List<TKey> _keys;
            private readonly Dictionary<TKey, MyGrouping<TKey, TElement>> _groupings;

            public MyLookup(IEqualityComparer<TKey> comparer)
            {
                _keys = new List<TKey>();
                _groupings = new Dictionary<TKey, MyGrouping<TKey, TElement>>(comparer);
            }

            internal void Add(TKey key, TElement element)
            {
                MyGrouping<TKey, TElement> grouping;
                if (!_groupings.TryGetValue(key, out grouping))
                {
                    grouping = new MyGrouping<TKey, TElement>(key);
                    _groupings.Add(key, grouping);
                    _keys.Add(key);
                }
                grouping.Add(element);
            }

            public IEnumerator<Linq.IGrouping<TKey, TElement>> GetEnumerator()
            {
                foreach (var key in _keys)
                {
                    yield return _groupings[key];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public bool Contains(TKey key)
            {
                return _groupings.ContainsKey(key);
            }

            public int Count
            {
                get { return _groupings.Count; }
            }

            public IEnumerable<TElement> this[TKey key]
            {
                get
                {
                    MyGrouping<TKey, TElement> grouping;
                    if (_groupings.TryGetValue(key, out grouping))
                    {
                        return grouping;
                    }
                    return Enumerable.Empty<TElement>();
                }
            }
        }

        private class MyGrouping<TKey, TElement> : Linq.IGrouping<TKey, TElement>
        {
            private readonly TKey _key;
            private readonly List<TElement> _elements;

            public MyGrouping(TKey key)
            {
                _key = key;
                _elements = new List<TElement>();
            }

            internal void Add(TElement element)
            {
                _elements.Add(element);
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return _elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public TKey Key
            {
                get { return _key; }
            }
        }
    }
}
