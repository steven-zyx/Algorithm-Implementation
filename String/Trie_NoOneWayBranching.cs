﻿using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class Trie_NoOneWayBranching<V> : IStringSymbolTable<V>
    {
        public bool IsEmpty => throw new NotImplementedException();

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public V Get(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Keys()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysThatMatch(string s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> KeysWithPrefix(string s)
        {
            throw new NotImplementedException();
        }

        public string LongestPrefixOf(string s)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, V value)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }
}
