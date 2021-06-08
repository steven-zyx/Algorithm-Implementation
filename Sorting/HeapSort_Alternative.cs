using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class HeapSort_Alternative
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
                int pos = SinkToBottom(0, --count);
                Swim(pos);
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

        private int SinkToBottom(int i, int count)
        {
            int item = _source[i];
            int child = (i + 1) * 2 - 1;
            while (child < count)
            {
                if (child + 1 < count && Less(child, child + 1))
                    child++;
                _source[i] = _source[child];
                i = child;
                child = (i + 1) * 2 - 1;
            }
            _source[i] = item;
            return i;
        }

        private void Swim(int i)
        {
            int parent = (i + 1) / 2 - 1;
            while (parent >= 0 && Less(parent, i))
            {
                Exchange(parent, i);
                i = parent;
                parent = (i + 1) / 2 - 1;
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
