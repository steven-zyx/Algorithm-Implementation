using System;
using System.Collections.Generic;
using System.Text;
using Searching;

namespace String
{
    public interface IStringSymbolTable<V> : ISymbolTable<string, V>
    {
        string LongestPrefixOf(string s);

        IEnumerable<string> KeysWithPrefix(string s);

        IEnumerable<string> KeysThatMatch(string s);
    }
}
