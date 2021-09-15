using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class BruteForceSubstringSearch
    {
        public static int Search_Approach1(string text, string pattern)
        {
            int tL = text.Length;
            int pL = pattern.Length;
            for (int i = 0; i <= tL - pL; i++)
            {
                int j = 0;
                for (; j < pL; j++)
                    if (!text[i + j].Equals(pattern[j]))
                        break;
                if (j == pL)
                    return i;
            }
            return tL;
        }

        public static int Search_Approach2(string text, string pattern)
        {
            int tL = text.Length, i = 0;
            int pL = pattern.Length, j = 0;
            for (; i < tL && j < pL; i++)
            {
                if (text[i].Equals(pattern[j]))
                    j++;
                else
                {
                    i -= j;
                    j = 0;
                }
            }
            if (j == pL)
                return i - pL;
            else
                return tL;
        }
    }
}
