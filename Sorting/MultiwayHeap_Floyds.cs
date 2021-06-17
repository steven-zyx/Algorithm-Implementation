using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MultiwayHeap_Floyds
    {
        private int[] _items;
        private int _count = 0;
        private int _nAry;
        private int _compareCount = 0;

        public MultiwayHeap_Floyds(int size, int nAry)
        {
            _items = new int[size + 1];
            _nAry = nAry;
        }

        public void Insert(int item)
        {
            _items[++_count] = item;
            Swim(_count);
        }

        public int DeleteMax()
        {
            int item = _items[1];
            Exchange(1, _count--);
            int pos = FloydsSink(1);
            Swim(pos);
            return item;
        }

        public int CompareCount => _compareCount;

        private void Swim(int i)
        {
            int parent = (i + _nAry - 2) / _nAry;
            while (parent > 0 && Greater(i, parent))
            {
                Exchange(parent, i);
                i = parent;
                parent = (i + _nAry - 2) / _nAry;
            }
        }

        private int FloydsSink(int i)
        {
            int item = _items[i];

            int child = i * _nAry - (_nAry - 2);
            while (child <= _count)
            {
                int greatest = child;
                for (int n = 1; n < _nAry && child + n <= _count; n++)
                {
                    if (Greater(child + n, greatest))
                        greatest = child + n;
                }

                _items[i] = _items[greatest];
                i = greatest;
                child = i * _nAry - (_nAry - 2);
            }
            _items[i] = item;
            return i;
        }

        private bool Greater(int i, int j)
        {
            _compareCount++;
            return _items[i] > _items[j];
        }

        private void Exchange(int i, int j)
        {
            int temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }

        #region Code for test
        private int _countForVerify;
        public bool TestConsistant()
        {
            _countForVerify = 0;
            return TestConsistant(1) && _countForVerify == _count;
        }

        public bool TestConsistant(int p)
        {
            if (p > _count)
                return true;

            _countForVerify++;
            bool isCon = true;
            int child = p * _nAry - (_nAry - 2);
            for (int n = 0; n < _nAry; n++)
            {
                if (child <= _count)
                    if (Greater(p, child))
                        isCon = TestConsistant(child++) && isCon;
                    else
                        return false;
                else
                    break;
            }
            return isCon;
        }
        #endregion
    }
}
