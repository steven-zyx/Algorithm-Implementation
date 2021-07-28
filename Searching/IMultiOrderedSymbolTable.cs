using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public interface IMultiOrderedSymbolTable<K, V> : IOrderedSymbolTable<K, V> where K : IComparable
    {
        IEnumerable<V> GetAll(K key);
    }
}
