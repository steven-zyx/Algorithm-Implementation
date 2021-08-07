using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class MSDStringSort : MSDSort<string>
    {
        protected override int CharAt(string text, int i)
        {
            if (i >= text.Length)
                return -1;
            else
                return _a.ToIndex(text[i]);
        }

        protected override bool Less(int a, int b, int d) => _source[a].Substring(d).CompareTo(_source[b].Substring(d)) < 0;

    }
}
