using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class KMP
    {
        protected const int R = 256;
        protected readonly int _m;
        protected int[,] _dfa;

        public KMP(string pattern)
        {
            _m = pattern.Length;
            _dfa = new int[R, _m];

            _dfa[pattern[0], 0] = 1;
            for (int x = 0, j = 1; j < _m; j++)
            {
                for (char c = (char)0; c < R; c++)
                    _dfa[c, j] = _dfa[c, x];
                _dfa[pattern[j], j] = j + 1;
                x = _dfa[pattern[j], x];
            }
        }

        public int Search(string text)
        {
            int i = 0, j = 0, l = text.Length;
            for (; i < l && j < _m; i++)
                j = _dfa[text[i], j];
            if (j == _m)
                return i - _m;
            else
                return text.Length;
        }
    }
}
