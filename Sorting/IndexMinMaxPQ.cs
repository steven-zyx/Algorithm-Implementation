using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class IndexMinMaxPQ
    {
        private int[] _minPQ;
        private int[] _minPosInMax;
        private int[] _maxPQ;
        private int[] _maxPosInMin;
        private int _count = 0;

        public IndexMinMaxPQ(int size)
        {
            _minPQ = new int[size + 1];
            _minPosInMax = new int[size + 1];
            _maxPQ = new int[size + 1];
            _maxPosInMin = new int[size + 1];
        }

        public void Insert(int item)
        {
            _count++;
            _minPQ[_count] = item;
            _maxPQ[_count] = item;
            SetReference(_count, _count);

            SwimForMinPQ(_count);
            SwimForMaxPQ(_count);
        }

        public int Min => _minPQ[1];

        public int DeleteMin()
        {
            int item = _minPQ[1];
            ExchangeMinPQ(1, _count--);
            SinkForMin(1);

            int maxIndex = _minPosInMax[_count + 1];
            ExchangeMaxPQ(maxIndex, _count + 1);
            SwimForMaxPQ(maxIndex);

            return item;
        }

        public int Max => _maxPQ[1];

        public int DeleteMax()
        {
            int item = _maxPQ[1];
            ExchangeMaxPQ(1, _count--);
            SinkForMax(1);

            int minIndex = _maxPosInMin[_count + 1];
            ExchangeMinPQ(minIndex, _count + 1);
            SwimForMinPQ(minIndex);

            return item;
        }

        public bool IsEmpty => _count == 0;

        private void SwimForMinPQ(int i)
        {
            while (i / 2 > 0 && Less(i, i / 2))
            {
                ExchangeMinPQ(i / 2, i);
                i /= 2;
            }
        }

        private void SwimForMaxPQ(int i)
        {
            while (i / 2 > 0 && Greater(i, i / 2))
            {
                ExchangeMaxPQ(i / 2, i);
                i /= 2;
            }
        }

        private void SinkForMin(int i)
        {
            while (i * 2 <= _count)
            {
                int j = i * 2;
                if (j + 1 <= _count && Less(j + 1, j))
                    j++;

                if (Less(j, i))
                {
                    ExchangeMinPQ(j, i);
                    i = j;
                }
                else
                    break;
            }
        }

        private void SinkForMax(int i)
        {
            while (i * 2 <= _count)
            {
                int j = i * 2;
                if (j + 1 <= _count && Greater(j + 1, j))
                    j++;

                if (Greater(j, i))
                {
                    ExchangeMaxPQ(j, i);
                    i = j;
                }
                else
                    break;
            }
        }

        private bool Less(int i, int j) => _minPQ[i] < _minPQ[j];

        private bool Greater(int i, int j) => _maxPQ[i] > _maxPQ[j];

        private void ExchangeMinPQ(int i, int j)
        {
            int temp = _minPQ[i];
            _minPQ[i] = _minPQ[j];
            _minPQ[j] = temp;

            temp = _minPosInMax[i];
            SetReference(i, _minPosInMax[j]);
            SetReference(j, temp);
        }

        private void ExchangeMaxPQ(int i, int j)
        {
            int temp = _maxPQ[i];
            _maxPQ[i] = _maxPQ[j];
            _maxPQ[j] = temp;

            temp = _maxPosInMin[i];
            SetReference(_maxPosInMin[j], i);
            SetReference(temp, j);
        }

        private void SetReference(int minPos, int maxPos)
        {
            _minPosInMax[minPos] = maxPos;
            _maxPosInMin[maxPos] = minPos;
        }
    }
}
