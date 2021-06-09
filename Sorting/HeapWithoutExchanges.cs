using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class HeapWithoutExchanges
    {
        private int[] _source;
        private int _count;
        private int _min;

        public HeapWithoutExchanges(int max)
        {
            _source = new int[max + 1];
        }

        public HeapWithoutExchanges() : this(15) { }

        public bool IsEmpty => _count == 0;
        public int Size => _count;

        public void Insert(int n)
        {
            _source[++_count] = n;
            Swim(_count);

            if (_count >= _source.Length - 1)
                Resize(_source.Length * 2);

            if (_count == 1 || n < _min)
                _min = n;
        }

        public int DelMax()
        {
            int result = _source[1];
            Exchange(1, _count--);
            Sink(1);

            if (_count < _source.Length / 4)
                Resize(_source.Length / 2);

            return result;
        }

        public int Max => _source[1];


        private void Exchange(int a, int b)
        {
            int temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }

        private bool Less(int a, int b) => _source[a] < _source[b];

        private void Swim(int i)
        {
            int item = _source[i];
            int parent = i / 2;
            while (parent > 0 && _source[parent] < item)
            {
                _source[i] = _source[parent];
                i = parent;
                parent = i / 2;
            }
            _source[i] = item;
        }

        private void Sink(int i)
        {
            int item = _source[i];
            int child = i * 2;
            while (child <= _count)
            {
                if (child + 1 <= _count && Less(child, child + 1))
                    child++;
                if (item < _source[child])
                {
                    _source[i] = _source[child];
                    i = child;
                    child = i * 2;
                }
                else
                    break;
            }
            _source[i] = item;
        }

        private void Resize(int size)
        {
            int[] newSource = new int[size];
            Array.Copy(_source, newSource, _count + 1);
            _source = newSource;
        }

        public int Min
        {
            get
            {
                if (_count == 0)
                    throw new InvalidOperationException("No element");
                else
                    return _min;
            }
        }
    }
}
