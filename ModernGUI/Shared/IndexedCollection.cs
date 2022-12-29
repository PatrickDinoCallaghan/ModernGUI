using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ModernGUI.Shared
{
    [Serializable]
    public class IndexedCollection<T> : IEnumerable<T>, INotifyCollectionChanged
    {
        public IndexedCollection()
        {
            _Collection = new Collection<T>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private Collection<T> _Collection = null;
        public T this[int index]
        {
            get
            {
                if (_Collection.Count <= index)
                {
                    return default(T);
                }
                else
                {
                    return _Collection[index];
                }
            }
            set
            {
                if (_Collection.Count <= index)
                {
                    for (int i = _Collection.Count; i <= index; i++)
                    {
                        if (i == index)
                        {
                            _Collection.Add(value);
                        }
                        else
                        {
                            _Collection.Add(default(T));
                        }
                    }

                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
                }
                else
                {
                    T oldValue = this[index];
                    _Collection[index] = value;
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldValue, index));
                }
            }
        }

        public void RemoveAt(int index)
        {

            if (_Collection.Count > index)
            {
                T oldValue = this[index];
                _Collection.RemoveAt(index);
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldValue, index));
            }
        }

        public void Remove(T val)
        {
            if (_Collection.Contains(val))
            {
                int IndexRemoved = _Collection.IndexOf(val);
                _Collection.Remove(val);
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, val, IndexRemoved));
            }
        }

        public void Clear()
        {
            _Collection.Clear();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {

            this.CollectionChanged?.Invoke(this, e);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Insert(int index, T Value)
        {
            if (_Collection.Count <= index)
            {
                this[index] = Value;
            }
            else
            {
                _Collection.Insert(index, Value);
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Value, index));
        }

        public int[] IndexOf(T Value)
        {
            List<int> RtnIndexs = new List<int>();

            for (int i = 0; i < _Collection.Count; i++)
            {

                if (_Collection[i] == null)
                {
                    if (Value == null)
                    {
                        RtnIndexs.Add(i);
                    }
                }
                else
                {
                    if (_Collection[i].Equals(Value))
                    {
                        RtnIndexs.Add(i);
                    }
                }

            }
            return RtnIndexs.ToArray();
        }

        public bool Contains(T Value)
        {
            return _Collection.Contains(Value);
        }

        public new string ToString()
        {
            return string.Join(",", _Collection.Where(r => r != null).Select(r => r.ToString() + "[" + String.Join(",", this.IndexOf(r)) + "]")).ToString() + "\nCount = " + _Collection.Count().ToString();
        }

    }
}
