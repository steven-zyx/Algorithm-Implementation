﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class OrderedSymbolTableCert<T, K, V>
        : SymbolTableCert<T, K, V>, IOrderedSymbolTable<K, V>
        where T : IOrderedSymbolTable<K, V>
        where K : IComparable
    {
        protected IOrderedSymbolTable<K, V> _ost;

        public OrderedSymbolTableCert(T ost) : base(ost)
        {
            _ost = ost;
        }

        public K Ceiling(K key) => _ost.Ceiling(key);

        public K Floor(K key) => _ost.Floor(key);

        public void DeleteMax()
        {
            _ost.DeleteMax();
            Cert.Certificate();
        }

        public void DeleteMin()
        {
            _ost.DeleteMin();
            Cert.Certificate();
        }

        public IEnumerable<K> Keys(K lo, K hi) => _ost.Keys(lo, hi);

        public K Max() => _ost.Max();

        public K Min() => _ost.Min();

        public int Rank(K key) => _ost.Rank(key);

        public K Select(int index) => _ost.Select(index);

        public int Size(K lo, K hi) => _ost.Size(lo, hi);
    }
}
