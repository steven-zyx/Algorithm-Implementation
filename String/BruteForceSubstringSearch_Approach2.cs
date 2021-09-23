using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class BruteForceSubstringSearch_Approach2 : SubStringSearch
    {
        protected string _pattern;

        public BruteForceSubstringSearch_Approach2(string pattern) : base(pattern)
            => _pattern = pattern;

        public override int Search(string text)
        {
            int N = text.Length, i = 0, j = 0;
            for (; i < N && j < M; i++)
            {
                if (text[i].Equals(_pattern[j]))
                    j++;
                else
                {
                    i -= j;
                    j = 0;
                }
            }
            if (j == M)
                return i - M;
            else
                return N;
        }
    }
}
