using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MinPQ_Stable<T> where T : IComparable
    {
        private T[] _items;
        private int[] _timeStamps;
        private int _order = 0;

        public MinPQ_Stable(int size)
        {
            _items = new T[size];
            _timeStamps = new int[size];
        }

        public void Insert()
        {

        }

        public T DeleteMin()
        {
            throw new NotImplementedException();
        }

        public T Min => _items[1];

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
