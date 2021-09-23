using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public abstract class SubStringSearch
    {
        protected int M;

        public SubStringSearch(string pattern)
        {
            M = pattern.Length;
        }

        public abstract int Search(string text);

        public virtual IEnumerable<int> FindAll(string text)
        {
            throw new NotImplementedException();
        }
    }
}
