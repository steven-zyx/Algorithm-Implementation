using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class HeapSort
    {
        private int[] _source;

        public void Sort(int[] source)
        {
            _source = source;
            int count = source.Length;
            for (int i = count / 2; i >= 0; i--)
                Sink(i, count);
            while (count > 1)
            {
                Exchange(0, count - 1);
                Sink(0, --count);
            }
        }

        private void Sink(int i, int count)
        {
            int child = (i + 1) * 2 - 1;
            while (child < count)
            {
                if (child + 1 < count && Less(child, child + 1))
                    child++;
                if (Less(i, child))
                {
                    Exchange(i, child);
                    i = child;
                    child = (i + 1) * 2 - 1;
                }
                else
                    break;
            }
        }

        private bool Less(int i, int j) => _source[i] < _source[j];

        private void Exchange(int i, int j)
        {
            int temp = _source[i];
            _source[i] = _source[j];
            _source[j] = temp;
        }
    }
}
