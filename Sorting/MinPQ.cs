using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MinPQ<T> where T : IComparable<T>
    {
        private T[] _source;
        private int _count;
        private IComparer<T> _comparer;

        public MinPQ(int max, IComparer<T> comparer) : this(max)
        {
            _comparer = comparer;
        }

        public MinPQ(int max)
        {
            _source = new T[max + 1];
        }

        public MinPQ() : this(15) { }

        public bool IsEmpty => _count == 0;
        public int Size => _count;

        public void Insert(T n)
        {
            _source[++_count] = n;
            Swim(_count);

            if (_count >= _source.Length - 1)
                Resize(_source.Length * 2);

        }

        public T DeleteMin()
        {
            T item = _source[1];
            Exchange(1, _count--);
            Sink(1);

            if (_count < _source.Length / 4)
                Resize(_source.Length / 2);

            return item;
        }

        public T Min => _source[1];

        private void Exchange(int a, int b)
        {
            T temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }

        private bool Less(int a, int b)
        {
            if (_comparer == null)
                return _source[a].CompareTo(_source[b]) < 0;
            else
                return _comparer.Compare(_source[a], _source[b]) < 0;
        }

        private void Swim(int i)
        {
            while (i > 1 && Less(i, i / 2))
            {
                Exchange(i / 2, i);
                i = i / 2;
            }
        }

        private void Sink(int i)
        {
            int j;
            while (i * 2 <= _count)
            {
                j = i * 2;
                if (j + 1 <= _count && Less(j + 1, j))
                    j++;
                if (Less(j, i))
                {
                    Exchange(i, j);
                    i = j;
                }
                else
                    break;
            }
        }

        private void Resize(int size)
        {
            T[] newSource = new T[size];
            Array.Copy(_source, newSource, _count + 1);
            _source = newSource;
        }
    }
}
