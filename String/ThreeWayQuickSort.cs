using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public abstract class ThreeWayQuickSort<T>
    {
        protected T[] _source;
        protected const int CUTOFF = 15;

        public void Sort(T[] source)
        {
            _source = source;
            Sort(0, _source.Length - 1, 0);
        }

        protected void Sort(int lo, int hi, int d)
        {
            if (lo + CUTOFF >= hi)
            {
                InsertionSort(lo, hi, d);
                return;
            }

            int lt = lo, gt = hi, i = lo + 1;
            int v = ValueAt(_source[lo], d);

            while (i <= gt)
            {
                int result = ValueAt(_source[i], d).CompareTo(v);
                if (result < 0)
                    Exchange(lt++, i++);
                else if (result > 0)
                    Exchange(gt--, i);
                else
                    i++;
            }

            Sort(lo, lt - 1, d);
            if (v >= 0) Sort(lt, gt, d + 1);
            Sort(gt + 1, hi, d);
        }

        protected void Exchange(int a, int b)
        {
            T temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }

        protected abstract int ValueAt(T element, int i);

        protected void InsertionSort(int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && Less(j, j - 1, d); j--)
                    Exchange(j, j - 1);
        }

        protected abstract bool Less(int a, int b, int d);
    }
}
