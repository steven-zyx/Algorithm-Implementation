using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class FastInsert
    {
        private int[] _source;
        private int _count;
        private int _min;

        public FastInsert(int max)
        {
            _source = new int[max + 1];
            _source[0] = int.MaxValue;
        }

        public FastInsert() : this(15) { }

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
            int[] parents = ShowParents(i);
            int gt = 0, le = parents.Length - 1;
            while (le - gt > 1)
            {
                int mid = gt + (le - gt) / 2;
                if (item < _source[parents[mid]])
                    gt = mid;
                else
                    le = mid;
            }

            for (int n = i; n / 2 >= parents[le]; n /= 2)
                _source[n] = _source[n / 2];
            _source[parents[le]] = item;
        }

        private int[] ShowParents(int i)
        {
            int length = (int)(Math.Log10(i) / Math.Log10(2)) + 2;
            int[] parents = new int[length];
            for (int n = length - 1; n >= 0; n--)
            {
                parents[n] = i;
                i /= 2;
            }
            return parents;
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
