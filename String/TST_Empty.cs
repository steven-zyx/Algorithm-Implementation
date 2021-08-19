using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public partial class TST_Empty<V> : TST<V>
    {
        protected V _valueOfEmpty;

        public TST_Empty() : base()
        {
            _valueOfEmpty = default(V);
        }

        public override V Get(string key)
        {
            if (key.Equals(""))
                return _valueOfEmpty;
            else
                return base.Get(key);
        }

        public override void Put(string key, V value)
        {
            if (key.Equals(""))
                _valueOfEmpty = value;
            else
                _root = Put(_root, key, 0, value);
        }

        public override bool IsEmpty => _root == null && _valueOfEmpty.Equals(default(V));

        public override bool Contains(string key)
        {
            if (key.Equals(""))
                return !_valueOfEmpty.Equals(default(V));
            else
                return base.Contains(key);
        }

        public override bool Delete(string key)
        {
            if (key.Equals(""))
                _valueOfEmpty = default(V);
            else
                _root = Delete(_root, key, 0);
            return true;
        }

        public override IEnumerable<string> Keys()
        {
            Queue<string> keys = new Queue<string>();
            if (!_valueOfEmpty.Equals(default(V)))
                keys.Enqueue("");
            Keys(_root, "", keys);
            return keys;
        }

        public override int Size()
        {
            int size = 0;
            if (!_valueOfEmpty.Equals(default(V)))
                size++;
            return size + Size(_root);
        }
    }

    //string dedicated operation
    public partial class TST_Empty<V>
    {
        public override IEnumerable<string> KeysThatMatch(string pat)
        {
            Queue<string> keys = new Queue<string>();
            if (pat.Equals(""))
                keys.Enqueue("");
            KeysThatMatch(_root, pat, "", keys);
            return keys;
        }

        public override IEnumerable<string> KeysWithPrefix(string prefix)
        {
            Queue<string> keys = new Queue<string>();
            if (prefix.Equals(""))
                keys.Enqueue("");
            TSTNode<V> node = Get(_root, prefix, 0);
            if (node != null)
                Keys(node.Mid, prefix, keys);
            return keys;
        }
    }
}

//5.2.8 Ordered operations for tries.Implement the floor(), ceil(), rank(), and
//select() (from our standard ordered ST API from Chapter 3) for TrieST.
