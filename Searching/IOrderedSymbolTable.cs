using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Searching
{
    public interface IOrderedSymbolTable<K, V> where K : IComparable
    {
        void Init();

        V this[K key] { get; set; }

        void Put(K key, V value);

        V Get(K key);

        bool Delete(K key);

        bool Contains(K key);

        bool IsEmpty();

        int Size();

        V Min { get; }

        V Max { get; }

        V Floor(K key);

        V Ceiling(K key);

        int Rank(K key);

        K Select(int index);

        void DeleteMin();

        void DeleteMax();

        int Size(K lo, K hi);

        IEnumerable Keys(K lo, K hi);

        IEnumerable Keys();
    }
}
