using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class KMP
    {
        protected const int R = 256;
        protected readonly int _m;
        protected int[][] _dfa = new int[256][];

        public KMP(string pattern)
        {
            _m = pattern.Length;
            for (int i = 0; i < R; i++)
                _dfa[i] = new int[_m];

            _dfa[pattern[0]][0] = 1;
            for (int x = 0, j = 1; j < _m; j++)
            {
                for (char c = (char)0; c < R; c++)
                    _dfa[c][j] = _dfa[c][x];
                _dfa[pattern[j]][j] = j + 1;
                x = _dfa[pattern[j]][x];
            }
        }

        public int Search(string text)
        {
            int i = 0, j = 0, n = text.Length;
            for (; i < n && j < _m; i++)
                j = _dfa[text[i]][j];
            if (j == _m)
                return i - _m;
            else
                return text.Length;
        }
    }
}
