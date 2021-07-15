using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public interface ISymbolTable<K, V>
    {
        void Init();

        V this[K key] { get; set; }

        V Get(K key);

        void Put(K key, V value);

        bool Delete(K key);

        bool Contains(K key);

        bool IsEmpty { get; }

        int Size();

        IEnumerable<K> Keys();
    }
}
