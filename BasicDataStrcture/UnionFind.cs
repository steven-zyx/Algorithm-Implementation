using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class UnionFind
    {
        public int Count { get; protected set; }
        protected int[] _id;
        protected int[] _size;

        public UnionFind(int n)
        {
            Count = n;

            _id = new int[Count];
            for (int i = 0; i < Count; i++)
                _id[i] = i;

            _size = new int[Count];
            for (int i = 0; i < Count; i++)
                _size[i] = 1;
        }

        public void Union(int v, int w)
        {
            int vRoot = Find(v), wRoot = Find(w);
            if (vRoot != wRoot)
            {
                if (_size[vRoot] < _size[wRoot])
                {
                    _id[vRoot] = w;
                    _size[wRoot] += _size[vRoot];
                }
                else
                {
                    _id[wRoot] = v;
                    _size[vRoot] += _size[wRoot];
                }
            }
        }

        protected int Find(int v)
        {
            while (v != _id[v])
                v = _id[v];
            return v;
        }

        public bool Connected(int v, int w) => Find(v) == Find(w);
    }
}
