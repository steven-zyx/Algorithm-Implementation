using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class IndexMinPQ
    {
        private int[] _pq;
        private int[] _keys;
        private int[] _inverse;
        private int _count = 0;

        public IndexMinPQ(int maxN)
        {
            _pq = new int[maxN + 1];
            for (int i = 0; i < _pq.Length; i++)
                _pq[i] = -1;
            _keys = new int[maxN];
            for (int i = 0; i < _keys.Length; i++)
                _keys[i] = -1;
            _inverse = new int[maxN];
            for (int i = 0; i < _inverse.Length; i++)
                _inverse[i] = -1;
        }

        public void Insert(int k, int item)
        {
            _keys[k] = item;
            _pq[++_count] = k;
            _inverse[k] = _count;
            Swim(_count);
        }

        public void Change(int k, int item)
        {
            _keys[k] = item;
            Swim(_inverse[k]);
            Sink(_inverse[k]);
        }

        public bool Contains(int k) => _inverse[k] > -1;

        public void Delete(int k)
        {
            int i = _inverse[k];
            Exchange(i, _count--);
            _inverse[k] = -1;
            Sink(i);
            Swim(i);
        }

        public int Min => _keys[MinIndex];

        public int MinIndex => _pq[1];

        public int DelMin()
        {
            int k = _pq[1];
            Exchange(1, _count--);
            Sink(1);
            _inverse[k] = -1;
            return k;
        }

        public bool IsEmpty => _count == 0;

        public int Size => _count;

        private bool Less(int i, int j) => _keys[_pq[i]] < _keys[_pq[j]];

        private void Exchange(int i, int j)
        {
            int temp = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = temp;

            _inverse[_pq[i]] = i;
            _inverse[_pq[j]] = j;
        }

        private void Swim(int i)
        {
            while (i > 1 && Less(i, i / 2))
            {
                Exchange(i, i / 2);
                i = i / 2;
            }
        }

        private void Sink(int i)
        {
            while (i * 2 <= _count)
            {
                int j = i * 2;
                if (j + 1 <= _count && Less(j + 1, j))
                    j++;
                if (Less(j, i))
                {
                    Exchange(j, i);
                    i = j;
                }
                else
                    break;
            }
        }
    }
}
