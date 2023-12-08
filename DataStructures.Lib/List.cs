using DataStructures.Lib.Interfaces;
using System;

namespace DataStructures.Lib
{
    public interface IIterator<out T>
    {
        T? Current { get; }

        bool MoveNext();

        void Reset();
    }

    public interface IIterable<out T>
    {
        IIterator<T> GetIterator();
    }

    public static class MyCoolLinq
    {
        private class FileterIterable<T> : IIterable<T>
        {
            private readonly IIterable<T> collection;
            private readonly Predicate<T> predicate;

            public FileterIterable(IIterable<T> collection, Predicate<T> predicate)
            {
                this.collection = collection;
                this.predicate = predicate;
            }

            public IIterator<T> GetIterator()
            {
                return new FilterIterator<T>(collection, predicate);
            }
        }

        private class FilterIterator<T> : IIterator<T>
        {
            private readonly IIterable<T> _collection;
            private readonly Predicate<T> _predicate;

            private IIterator<T> _iterator;

            public T? Current => _iterator.Current;

            public FilterIterator(IIterable<T> collection, Predicate<T> predicate)
            {
                this._collection = collection;
                this._predicate = predicate;
            }

            public bool MoveNext()
            {
                if (_iterator == null) _iterator = _collection.GetIterator();

                bool result = false;
                do
                {
                    result = _iterator.MoveNext();
                    if (result && _predicate(_iterator.Current))
                    {
                        return true;
                    }
                } while (result);

                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        private class TakeIterable<T> : IIterable<T>
        {
            private readonly IIterable<T> collection;
            private readonly int count;

            public TakeIterable(IIterable<T> collection, int count)
            {
                this.collection = collection;
                this.count = count;
            }

            public IIterator<T> GetIterator()
            {
                return new TakeIterator<T>(collection, count);
            }
        }

        private class TakeIterator<T> : IIterator<T>
        {
            private readonly IIterable<T> _collection;
            private readonly int count;

            private IIterator<T> _iterator;
            private int currentCount;

            public T? Current => _iterator.Current;

            public TakeIterator(IIterable<T> collection, int count)
            {
                this._collection = collection;
                this.count = count;
            }

            public bool MoveNext()
            {
                if (_iterator == null) _iterator = _collection.GetIterator();

                while (_iterator.MoveNext() && currentCount < count)
                {
                    currentCount++;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public static IIterable<T> Filter<T>(this IIterable<T> iterable, Predicate<T> predicate)
        {
            return new FileterIterable<T>(iterable, predicate);
        }

        public static IIterable<T> Take<T>(this IIterable<T> iterable, int count)
        {
            return new TakeIterable<T>(iterable, count);
        }
    }


    public class List<T> : Interfaces.IList<T>, IIterable<T>
    {
        private const int defaultCapacity = 4;

        private readonly T?[] emptyArray = Array.Empty<T?>();

        private T?[] _data;

        public int Capacity => _data.Length;

        public int Count { get; private set; } = 0;

        public List()
        {
            _data = new T[defaultCapacity];
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (capacity == 0)
            {
                _data = emptyArray;
            }
            else
            {
                _data = new T[capacity];
            }
        }

        public T? this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _data[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            set
            {
                if (index < Count)
                {
                    _data[index] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void TryGrow()
        {
            if (Count + 1 >= Capacity)
            {
                T?[] array = _data;
                _data = new T[array.Length * 2];
                for (int i = 0; i < Count; i++)
                {
                    _data[i] = array[i];
                }
            }
        }

        public void Add(T? value)
        {
            TryGrow();

            _data[Count] = value;
            Count++;
        }

        public void Insert(int index, T? value)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            TryGrow();

            for (int i = Count - 1; i >= index; i--)
            {
                var currentItem = _data[i];
                _data[i + 1] = currentItem;
            }

            _data[index] = value;
            Count++;
        }

        public void Remove(T? value)
        {
            int index = IndexOf(value);
            if (index != -1) RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = index; i < Count - 1; i++)
            {
                var nextItem = _data[i + 1];
                _data[i] = nextItem;
            }

            _data[--Count] = default;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++) _data[i] = default;
            //_data = emptyArray;
            Count = 0;
        }

        public bool Contains(T? value) => IndexOf(value) != -1;

        public int IndexOf(T? value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(_data[i], value))
                    return i;
            }
            return -1;
        }

        public T?[] ToArray()
        {
            if (Count == 0) return emptyArray;

            T?[] array = new T?[Count];
            for (int i = 0; i < array.Length; i++)
                array[i] = _data[i];

            return array;
        }

        public void Reverse()
        {
            T? first, last;
            for (int i = 0; i < Count / 2; i++)
            {
                first = _data[i];
                last = _data[Count - i - 1];
                _data[i] = last;
                _data[Count - i - 1] = first;
            }
        }

        public IIterator<T> GetIterator()
        {
            return new ListIterator<T>(this);
        }

        private class ListIterator<TItem> : IIterator<TItem>
        {
            public TItem? Current { get; private set; }

            private int currentIndex = 0;
            private readonly List<TItem> list;

            public ListIterator(List<TItem> list)
            {
                this.list = list;
            }

            public bool MoveNext()
            {
                if (currentIndex < list.Count)
                {
                    Current = list[currentIndex];
                    currentIndex++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                currentIndex = 0;
            }
        }
    }
}
