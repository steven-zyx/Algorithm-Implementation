using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MinPQ_Stable<T> where T : IComparable
    {
        private T[] _items;
        private int[] _timeStamps;
        private int _count = 0;
        private int _order = 0;

        public MinPQ_Stable(int size)
        {
            _items = new T[size + 1];
            _timeStamps = new int[size + 1];
        }

        public void Insert(T item)
        {
            _count++;
            _items[_count] = item;
            _timeStamps[_count] = _order++;
            Swim(_count);
        }

        public T DeleteMin()
        {
            T item = _items[1];
            Exchange(1, _count--);
            Sink(1);
            return item;
        }

        public T Min => _items[1];

        private void Swim(int i)
        {
            while (i / 2 >= 1 && Less(i, i / 2))
            {
                Exchange(i / 2, i);
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

        private bool Less(int a, int b)
        {
            int result = _items[a].CompareTo(_items[b]);
            if (result == 0)
                return _timeStamps[a] < _timeStamps[b];
            else
                return result < 0;
        }

        private void Exchange(int a, int b)
        {
            T tempItem = _items[a];
            _items[a] = _items[b];
            _items[b] = tempItem;

            int tempTimeStamp = _timeStamps[a];
            _timeStamps[a] = _timeStamps[b];
            _timeStamps[b] = tempTimeStamp;
        }
    }
}
