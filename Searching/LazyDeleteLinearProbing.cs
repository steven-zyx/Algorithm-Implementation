using System;
using System.Collections.Generic;
using System.Text;

namespace Searching
{
    public class LazyDeleteLinearProbing<K, V> : LinearProbingHashST<K, V> where K : struct where V : struct
    {
        protected int _tombStone;

        public LazyDeleteLinearProbing() : base()
        {
            _tombStone = 0;
        }

        public LazyDeleteLinearProbing(int size) : base(size)
        {
            _tombStone = 0;
        }

        public override bool Delete(K key)
        {
            for (int i = Hash(key); _keys[i].HasValue; Increment(ref i))
                if (key.Equals(_keys[i]))
                {
                    if (_values[i].HasValue)
                    {
                        _values[i] = null;
                        _count--;
                        _tombStone++;

                        if (_count < M / 8)
                            Resize(M / 2);
                        return true;
                    }
                    else
                        return false;
                }
            return false;
        }

        public override V Get(K key)
        {
            for (int i = Hash(key); _keys[i].HasValue; Increment(ref i))
                if (key.Equals(_keys[i]))
                    if (_values[i].HasValue)
                        return _values[i].Value;
                    else
                        return default(V);
            return default(V);
        }

        public override void Put(K key, V value)
        {
            int i;
            for (i = Hash(key); _keys[i].HasValue; Increment(ref i))
                if (key.Equals(_keys[i]))
                {
                    if (!_values[i].HasValue)
                        _tombStone--;
                    _values[i] = value;
                    return;
                }

            _keys[i] = key;
            _values[i] = value;
            _count++;

            if (_count + _tombStone > M / 2)
                Resize(M * 2);
        }

        protected override void Resize(int size)
        {
            LazyDeleteLinearProbing<K, V> newST = new LazyDeleteLinearProbing<K, V>(size);

            for (int i = 0; i < M; i++)
                if (_keys[i].HasValue && _values[i].HasValue)
                    newST.Put(_keys[i].Value, _values[i].Value);

            _keys = newST._keys;
            _values = newST._values;
            M = size;
            _tombStone = 0;
        }
    }
}
