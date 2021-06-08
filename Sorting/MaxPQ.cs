using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MaxPQ
    {
        private int[] _source;
        private int _count;
        private int _min;

        public MaxPQ(int max)
        {
            _source = new int[max + 1];
        }

        public MaxPQ()
        {
            _source = new int[16];
        }

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
            while (i > 1 && Less(i / 2, i))
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
                if (j + 1 <= _count && Less(j, j + 1))
                    j++;
                if (Less(i, j))
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
