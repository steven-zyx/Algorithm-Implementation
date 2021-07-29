using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public interface ISet<K>
    {
        void Put(K key);

        bool Delete(K key);

        bool Contains(K key);

        bool IsEmpty { get; }

        int Size();

        IEnumerable<K> Keys();
    }
}
