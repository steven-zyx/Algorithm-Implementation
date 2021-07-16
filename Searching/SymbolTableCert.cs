using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class SymbolTableCert<T, K, V> : ISymbolTable<K, V> where T : ISymbolTable<K, V>
    {
        protected ISymbolTable<K, V> _st;
        protected ICertificate Cert => _st as ICertificate;

        public SymbolTableCert(T st)
        {
            _st = st;
        }

        public V this[K key]
        {
            get => _st[key];
            set => _st[key] = value;
        }

        public bool IsEmpty => _st.IsEmpty;

        public bool Contains(K key) => _st.Contains(key);

        public bool Delete(K key)
        {
            bool result = _st.Delete(key);
            Cert.Certificate();
            return result;
        }

        public V Get(K key) => _st.Get(key);

        public void Init() => _st.Init();

        public IEnumerable<K> Keys() => _st.Keys();

        public void Put(K key, V value)
        {
            _st.Put(key, value);
            Cert.Certificate();
        }

        public int Size() => _st.Size();
    }
}
