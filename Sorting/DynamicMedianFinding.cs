using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class DynamicMedianFinding
    {
        private MaxPQ _maxPQ = new MaxPQ();
        private MinPQ<int> _minPQ = new MinPQ<int>();

        public void Insert(int item)
        {
            if (_maxPQ.IsEmpty)
                _maxPQ.Insert(item);
            else if (_minPQ.IsEmpty)
                _minPQ.Insert(item);
            else if (item >= _maxPQ.Max)
                _minPQ.Insert(item);
            else
                _maxPQ.Insert(item);

            while (Math.Abs(_maxPQ.Size - _minPQ.Size) > 1)
            {
                if (_maxPQ.Size > _minPQ.Size)
                    _minPQ.Insert(_maxPQ.DelMax());
                else
                    _maxPQ.Insert(_minPQ.DeleteMin());
            }
        }

        public int Median
        {
            get
            {
                if (_maxPQ.Size > _minPQ.Size)
                    return _maxPQ.Max;
                else
                    return _minPQ.Min;
            }
        }

        public int DeleteMedian()
        {
            if (_maxPQ.Size > _minPQ.Size)
                return _maxPQ.DelMax();
            else
                return _minPQ.DeleteMin();
        }

        public int Count => _maxPQ.Size + _minPQ.Size;
    }
}
