using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public interface IMultiSymbolTable<K, V> : ISymbolTable<K, V>
    {
        IEnumerable<V> GetAll(K key);
    }
}
