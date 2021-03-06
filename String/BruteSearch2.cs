using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class BruteSearch2 : SubStringSearch
    {
        protected string _pattern;

        public BruteSearch2(string pattern) : base(pattern)
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

        public override IEnumerable<int> FindAll(string text)
        {
            int N = text.Length, j = 0;
            for (int i = 0; i < N; i++)
            {
                if (text[i].Equals(_pattern[j]))
                    j++;
                else
                {
                    i -= j;
                    j = 0;
                }

                if (j == M)
                {
                    i = i - j + 1;
                    yield return i;
                    j = 0;
                }
            }
        }
    }
}
