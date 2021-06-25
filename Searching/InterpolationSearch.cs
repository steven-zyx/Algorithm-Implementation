using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class InterpolationSearch<V> : BinarySearchST<int, V>
    {
        public override int Rank(int key)
        {
            int lo = 0, hi = _count - 1;
            while (hi >= lo)
            {
                int mid;
                if (key >= Max())
                    mid = hi;
                else if (key <= Min())
                    mid = lo;
                else
                {
                    float temp = (float)(key - Min()) / (Max() - Min()) * (hi - lo) + lo;
                    mid = (int)temp;
                }

                int dif = key.CompareTo(_keys[mid]);
                if (dif == 0) return mid;
                else if (dif < 0) hi = mid - 1;
                else lo = mid + 1;
            }
            return lo;
        }
    }
}
