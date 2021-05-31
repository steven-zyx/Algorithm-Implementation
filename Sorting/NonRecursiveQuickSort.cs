using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class NonRecursiveQuickSort
    {
        private int[] _source;
        private Stack<int> _loStack = new Stack<int>();
        private Stack<int> _hiStack = new Stack<int>();

        public void Sort(int[] source)
        {
            _source = source;
            _loStack.Push(0);
            _hiStack.Push(_source.Length - 1);

            while (_loStack.Count > 0)
            {
                int lo = _loStack.Pop();
                int hi = _hiStack.Pop();
                if (hi <= lo)
                    continue;
                int mid = Partition(lo, hi);

                if (mid - lo > hi - mid)
                {
                    _loStack.Push(lo);
                    _hiStack.Push(mid - 1);
                    _loStack.Push(mid + 1);
                    _hiStack.Push(hi);
                }
                else
                {
                    _loStack.Push(mid + 1);
                    _hiStack.Push(hi);
                    _loStack.Push(lo);
                    _hiStack.Push(mid - 1);
                }
            }
        }

        private int Partition(int lo, int hi)
        {
            int leftIndex = lo;
            int rightIndex = hi + 1;
            int midE = _source[lo];

            while (true)
            {
                while (_source[++leftIndex] < midE)
                    if (leftIndex >= hi)
                        break;
                while (midE < _source[--rightIndex])
                    if (rightIndex <= lo)
                        break;
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
