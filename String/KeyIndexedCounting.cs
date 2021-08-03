using System;

namespace String
{
    public class KeyIndexedCounting
    {
        public void Sort(int[] source, int R)
        {
            int count = source.Length;
            int[] frequency = new int[R + 1];
            for (int i = 0; i < count; i++)
                frequency[source[i] + 1]++;
            for (int i = 2; i < frequency.Length; i++)
                frequency[i] = frequency[i] + frequency[i - 1];

            int[] aux = new int[count];
            for (int i = 0; i < count; i++)
                aux[frequency[source[i]]++] = source[i];
            for (int i = 0; i < count; i++)
                source[i] = aux[i];
        }
    }
}
