using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class SubStringMatches
    {
        protected TST<string> _st;

        public SubStringMatches(IEnumerable<string> stringList)
        {
            _st = new TST<string>();
            foreach (string str in stringList)
                for (int i = 0; i < str.Length; i++)
                    _st.Put(str.Substring(i), str);
        }

        public IEnumerable<string> StringThatContains(string s)
        {
            IEnumerable<string> keys = _st.KeysWithPrefix(s);
            HashSet<string> stringList = new HashSet<string>();
            foreach (string key in keys)
                stringList.Add(_st.Get(key));
            return stringList;
        }
    }
}
