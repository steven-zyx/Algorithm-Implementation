using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class HybridSort : MSDSort
    {
        protected override void Sort(int lo, int hi, int d)
        {
            int[] count = new int[_a.R + 2];
            for (int i = lo; i <= hi; i++)
                count[CharAt(_source[i], d) + 2]++;
            for (int i = 2; i < count.Length; i++)
                count[i] += count[i - 1];
            for (int i = lo; i <= hi; i++)
                _aux[count[CharAt(_source[i], d) + 1]++] = _source[i];
            for (int i = lo; i <= hi; i++)
                _source[i] = _aux[i - lo];

            for (int i = 0; i < _a.R; i++)
            {
                int newLo = lo + count[i];
                int newHi = lo + count[i + 1] - 1;
                int size = newHi - newLo + 1;

                if (size <= 1)
                    continue;
                else if (size <= CUTOFF)
                    InsertionSort(newLo, newHi, d + 1);
                else if (size >= hi - lo + 1 - 3)
                    Quick3WaySort(newLo, newHi, d + 1);
                else
                    Sort(newLo, newHi, d + 1);
            }
        }

        private void Quick3WaySort(int lo, int hi, int d)
        {
            if (lo + CUTOFF >= hi)
            {
                InsertionSort(lo, hi, d);
                return;
            }

            int lt = lo, gt = hi, i = lo + 1;
            int v = CharAt4QuickSort(_source[lo], d);

            while (i <= gt)
            {
                int result = CharAt4QuickSort(_source[i], d).CompareTo(v);
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

        protected int CharAt4QuickSort(string text, int i)
        {
            if (i >= text.Length)
                return -1;
            else
                return text[i];
        }
    }
}
