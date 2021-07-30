using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class Concordance
    {
        private ISymbolTable<string, List<int>> _st;

        public Concordance(string[] words)
        {
            _st = new SeperateChainingHashST<string, List<int>>();
            Build(words);
        }

        private void Build(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (_st.Get(words[i]) == null)
                    _st.Put(words[i], new List<int>() { i });
                else
                    _st.Get(words[i]).Add(i);
            }
        }

        public ISymbolTable<string, List<int>> WordsOccurance() => _st;

        public string[] Invert(ISymbolTable<string, List<int>> st)
        {
            int count = 0;
            foreach (string key in st.Keys())
                count += st.Get(key).Count;

            string[] sequence = new string[count];
            foreach (string key in st.Keys())
                foreach (int i in st.Get(key))
                    sequence[i] = key;
            return sequence;
        }
    }
}
