using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;

namespace FlickIt
{
    public class ObservableQueue<T> : ICollection, IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private Queue<T> queue;

        #region Constructors

        public ObservableQueue()
        {
            queue = new Queue<T>();
        }

        public ObservableQueue(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        public ObservableQueue(IEnumerable<T> collection)
        {
            queue = new Queue<T>(collection);
        } 

        #endregion

        #region Queue methods

        public void Clear()
        {
            queue.Clear();
            NotifyCollectionChangedEventArgs e =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(e);
        }

        public bool Contains(T item)
        {
            return queue.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            queue.CopyTo(array, arrayIndex);
        }

        public T Dequeue()
        {
            T item = queue.Dequeue();
            NotifyCollectionChangedEventArgs e =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(e);
            return item;
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
            NotifyCollectionChangedEventArgs e =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(e);
        }

        public Queue<T>.Enumerator GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        public T Peek()
        {
            return queue.Peek();
        }

        public T[] ToArray()
        {
            return queue.ToArray();
        }

        public void TrimExcess()
        {
            queue.TrimExcess();
        }

        #endregion

        #region ICollection implementation

        int ICollection.Count
        {
            get { return queue.Count; }
        }

        #endregion

        #region ICollection explicit implementation

        void  ICollection.CopyTo(Array array, int index)
        {
            (queue as ICollection).CopyTo(array, index);
        }

        bool  ICollection.IsSynchronized
        {
            get { return (queue as ICollection).IsSynchronized; }
        }

        object  ICollection.SyncRoot
        {
            get { return (queue as ICollection).SyncRoot; }
        }

        #endregion

        #region IEnumerable<T> explicit implementation

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        #endregion

        #region IEnumerable explicit implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (queue as ICollection).GetEnumerator();
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyCollectionChanged implementation

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);

        }

        #endregion

    
    }
}
