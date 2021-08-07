using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class MSDIntSort : MSDSort<int>
    {
        protected override void Sort(int lo, int hi, int d)
        {
            int[] count = new int[16_777_216 + 2];
            for (int i = lo; i <= hi; i++)
                count[CharAt(_source[i], d) + 2]++;
            for (int i = 2; i < count.Length; i++)
                count[i] += count[i - 1];
            for (int i = lo; i <= hi; i++)
                _aux[count[CharAt(_source[i], d) + 1]++] = _source[i];
            for (int i = lo; i <= hi; i++)
                _source[i] = _aux[i - lo];

            for (int i = 0; i < count.Length - 1; i++)
                if (count[i + 1] - count[i] >= 1)
                    InsertionSort(count[i], count[i + 1] - 1, d + 1);
        }

        protected override int CharAt(int text, int i) => text >> 8;

        protected override bool Less(int a, int b, int d) =>
            _source[a].CompareTo(_source[b]) < 0;
    }
}
