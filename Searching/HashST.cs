using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    //Hast symbol table dedicated functions and fields
    public abstract partial class HashST<K, V> : ISymbolTable<K, V>
    {
        protected int M;

        protected HashST(int m)
        {
            M = m;
        }

        protected int Hash(K key) => (key.GetHashCode() & 0x7fff_ffff) % M;
    }

    //Implement the Interface, most abstractly
    public abstract partial class HashST<K, V>
    {
        public abstract bool IsEmpty { get; }

        public abstract bool Contains(K key);

        public abstract bool Delete(K key);

        public abstract V Get(K key);

        public abstract IEnumerable<K> Keys();

        public abstract void Put(K key, V value);

        public abstract int Size();
    }
}
