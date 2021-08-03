using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class LSDSort
    {
        public void Sort(string[] source, int w)
        {
            int count = source.Length;
            string[] aux = new string[count];
            for (int i = w - 1; i >= 0; i--)
            {
                int[] frequence = new int[65537];
                for (int j = 0; j < count; j++)
                    frequence[source[j][i] + 1]++;
                for (int j = 2; j < frequence.Length; j++)
                    frequence[j] = frequence[j - 1] + frequence[j];
                for (int j = 0; j < count; j++)
                    aux[frequence[source[j][i]]++] = source[j];
                for (int j = 0; j < count; j++)
                    source[j] = aux[j];
            }
        }
    }
}
