using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class BoyerMoore
    {
        protected const int R = 256;
        protected string _pattern;
        protected int M;
        protected int N;
        protected int[] _right;

        public BoyerMoore(string pattern)
        {
            _pattern = pattern;
            M = pattern.Length;

            _right = new int[R];
            for (int i = 0; i < R; i++)
                _right[i] = -1;
            for (int j = 0; j < M; j++)
                _right[pattern[j]] = j;
        }

        public int Search(string text)
        {
            N = text.Length;

            int skip = 0;
            for (int i = 0; i <= N - M; i += skip)
            {
                skip = 0;
                for (int j = M - 1; j >= 0; j--)
                {
                    if (!text[i + j].Equals(_pattern[j]))
                    {
                        skip = j - _right[text[i + j]];
                        if (skip < 1)
                            skip = 1;
                        break;
                    }
                }
                if (skip == 0)
                    return i;
            }
            return N;
        }
    }
}
