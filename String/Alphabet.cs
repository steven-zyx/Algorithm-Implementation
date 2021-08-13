using System;
using System.Collections.Generic;
using System.Text;
using Searching;

namespace String
{
    public class Alphabet
    {
        private ISymbolTable<char, int> _st;
        private char[] _charcters;

        public char[] Charcters => _charcters;

        public Alphabet(string s)
        {
            _charcters = s.ToCharArray();
            Array.Sort(_charcters);

            _st = new CuckooHashing<char, int>();
            for (int i = 0; i < _charcters.Length; i++)
                _st.Put(_charcters[i], i);
        }

        public char ToChar(int index) => _charcters[index];

        public int ToIndex(char c) => _st.Get(c);

        public bool Contains(char c) => _st.Contains(c);

        public int R => _st.Size();

        public int[] ToIndices(string s)
        {
            int[] indices = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
                indices[i] = _st.Get(s[i]);
            return indices;
        }

        public string ToChars(int[] indices)
        {
            char[] cArray = new char[indices.Length];
            for (int i = 0; i < indices.Length; i++)
                cArray[i] = _charcters[indices[i]];
            return new string(cArray);
        }
    }
}
