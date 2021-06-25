using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Searching
{
    public interface IOrderedSymbolTable<K, V> : ISymbolTable<K, V> where K : IComparable
    {
        K Min { get; }

        K Max { get; }

        V Floor(K key);

        V Ceiling(K key);

        int Rank(K key);

        K Select(int index);

        void DeleteMin();

        void DeleteMax();

        int Size(K lo, K hi);

        IEnumerable Keys(K lo, K hi);
    }
}
