using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class ShellSort
    {
        public void Sort<T>(T[] source) where T : IComparable
        {
            int length = source.Length;

            int h = 1;
            while (h < length / 3) h = h * 3 + 1;
            while (h >= 1)
            {
                for (int i = h; i < length; i++)
                {
                    for (int j = i; j >= h && source[j - h].CompareTo(source[j]) > 0; j -= h)
                    {
                        Exchange(source, j - h, j);
                    }
                }
                h /= 3;
            }
        }

        public void Sort_2(byte[] source)
        {
            int length = source.Length;

            byte h = 40;
            while (h >= 1)
            {
                for (byte i = h; i < length; i++)
                {
                    for (byte j = i; j >= h && source[j - h].CompareTo(source[j]) > 0; j -= h)
                    {
                        Exchange(source, j - h, j);
                    }
                }
                h /= 3;
            }
        }

        private void Exchange<T>(T[] source, int i, int j)
        {
            T item = source[i];
            source[i] = source[j];
            source[j] = item;
        }
    }
}
