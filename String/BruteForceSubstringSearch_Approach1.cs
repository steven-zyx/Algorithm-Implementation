using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class BruteForceSubstringSearch_Approach1 : SubStringSearch
    {
        protected string _pattern;

        public BruteForceSubstringSearch_Approach1(string pattern) : base(pattern)
            => _pattern = pattern;

        public override int Search(string text)
        {
            int N = text.Length;
            for (int i = 0; i <= N - M; i++)
            {
                int j = 0;
                for (; j < M; j++)
                    if (!text[i + j].Equals(_pattern[j]))
                        break;
                if (j == M)
                    return i;
            }
            return N;
        }
    }
}
