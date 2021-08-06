using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class ThreeWayStringQuickSort
    {
        private string[] _source;
        private const int CUTOFF = 15;

        public void Sort(string[] source)
        {
            _source = source;
            Sort(0, _source.Length - 1, 0);
        }

        private void Sort(int lo, int hi, int d)
        {
            if (lo + CUTOFF >= hi)
            {
                InsertionSort(lo, hi, d);
                return;
            }

            int lt = lo, gt = hi, i = lo + 1;
            int v = CharAt(_source[lo], d);

            while (i <= gt)
            {
                int result = CharAt(_source[i], d).CompareTo(v);
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

        private void Exchange(int a, int b)
        {
            string temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }

        private int CharAt(string text, int i)
        {
            if (i >= text.Length)
                return -1;
            else
                return text[i];
        }

        private void InsertionSort(int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && Less(j, j - 1, d); j--)
                    Exchange(j, j - 1);
        }

        private bool Less(int a, int b, int d) => _source[a].Substring(d).CompareTo(_source[b].Substring(d)) < 0;
    }
}
