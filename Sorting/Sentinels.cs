using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class Sentinels
    {
        private int[] _source;

        public void Sort(int[] input)
        {
            int size = input.Length;

            _source = new int[size + 1];
            Array.Copy(input, _source, size);
            _source[size] = int.MaxValue;
            Sort(0, size);
            Array.Copy(_source, input, size);
        }

        private void Sort(int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = Partition(lo, hi);
            Sort(lo, mid - 1);
            Sort(mid + 1, hi);
        }

        private int Partition(int lo, int hi)
        {
            int leftIndex = lo;
            int rightIndex = hi + 1;
            int midE = _source[lo];

            while (true)
            {
                while (_source[++leftIndex] < midE) { }
                while (midE < _source[--rightIndex]) { }
                if (leftIndex >= rightIndex)
                    break;
                Exchange(leftIndex, rightIndex);
            }

            Exchange(lo, rightIndex);
            return rightIndex;
        }

        private void Exchange(int i, int j)
        {
            int item = _source[i];
            _source[i] = _source[j];
            _source[j] = item;
        }
    }
}
