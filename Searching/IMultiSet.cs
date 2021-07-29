using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public interface IMultiSet<K> : ISet<K>
    {
        int Count(K key);

        int DeleteAll(K key);
    }
}
