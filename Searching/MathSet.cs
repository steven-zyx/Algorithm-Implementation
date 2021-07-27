using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Searching
{
    public class MathSet<K> where K : struct
    {
        private ISymbolTable<K, bool> _set;
        private ISymbolTable<K, bool> _universe;

        public MathSet(IEnumerable<K> universe)
        {
            _set = new LinearProbingHashST<K, bool>();
            _universe = new CuckooHashing<K, bool>();
            foreach (K key in universe)
                _universe.Put(key, false);
        }

        public IEnumerable<K> Complement()
        {
            foreach (K key in _universe.Keys())
                if (!_set.Contains(key))
                    yield return key;
        }

        public void Union(IEnumerable<K> a)
        {
            foreach (K key in a)
                if (!_set.Contains(key))
                    _set.Put(key, false);
        }

        public void Intersection(IEnumerable<K> a)
        {
            K[] keys = _set.Keys().ToArray();
            foreach (K key in keys)
                if (!a.Contains(key))
                    _set.Delete(key);
        }

        public void Add(K key)
        {
            if (!_set.Contains(key)) _set.Put(key, false);
        }

        public void Delete(K key) => _set.Delete(key);

        public bool Contains(K key) => _set.Contains(key);

        public bool IsEmpty() => _set.Size() == 0;

        public int Size() => _set.Size();

        public IEnumerable<K> Keys() => _set.Keys();
    }
}
