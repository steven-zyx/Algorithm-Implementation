using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class KMP : SubStringSearch
    {
        protected const int R = 256;
        protected int[,] _dfa;

        public KMP(string pattern) : base(pattern)
        {
            _dfa = new int[R, M];

            _dfa[pattern[0], 0] = 1;
            for (int x = 0, j = 1; j < M; j++)
            {
                for (char c = (char)0; c < R; c++)
                    _dfa[c, j] = _dfa[c, x];
                _dfa[pattern[j], j] = j + 1;
                x = _dfa[pattern[j], x];
            }
        }

        public override int Search(string text)
        {
            int i = 0, j = 0, l = text.Length;
            for (; i < l && j < M; i++)
                j = _dfa[text[i], j];
            if (j == M)
                return i - M;
            else
                return text.Length;
        }

        public override IEnumerable<int> FindAll(string text)
        {
            int j = 0, N = text.Length;
            for (int i = 0; i < N; i++)
            {
                j = _dfa[text[i], j];
                if (j == M)
                {
                    i = i - M + 1;
                    yield return i;
                    j = 0;
                }
            }
        }
    }
}
