using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace String
{
    public class MSDSort
    {
        protected string[] _source;
        protected string[] _aux;
        protected Alphabet _a;
        protected const int CUTOFF = 15;

        public void Sort(string[] source, Alphabet a)
        {
            _source = source;
            _aux = new string[source.Length];
            _a = a;
            Sort(0, source.Length - 1, 0);
        }

        protected virtual void Sort(int lo, int hi, int d)
        {
            if (lo + CUTOFF > hi)
            {
                InsertionSort(lo, hi, d);
                return;
            }

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
                if (count[i + 1] - count[i] > 1)
                    Sort(lo + count[i], lo + count[i + 1] - 1, d + 1);
        }

        protected int CharAt(string text, int i)
        {
            if (i >= text.Length)
                return -1;
            else
                return _a.ToIndex(text[i]);
        }

        protected bool Less(int a, int b, int d) => _source[a].Substring(d).CompareTo(_source[b].Substring(d)) < 0;

        protected void Exchange(int a, int b)
        {
            string temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }

        protected void InsertionSort(int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && Less(j, j - 1, d); j--)
                    Exchange(j, j - 1);
        }
    }
}
