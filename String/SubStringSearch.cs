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

        public virtual int Search(string text)
        {
            throw new NotFiniteNumberException();
        }
    }
}
