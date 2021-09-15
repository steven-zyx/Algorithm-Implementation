﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Bag_L<V> : IEnumerable<V>
    {
        protected Node<V> _root;
        protected int _count;

        public Bag_L()
        {
            _count = 0;
        }

        public int Size => _count;

        public bool IsEmpty => _count == 0;

        public void Add(V value)
        {
            Node<V> n = new Node<V>(value);
            n.Next = _root;
            _root = n;
            _count++;
        }

        public IEnumerator<V> GetEnumerator()
        {
            for (Node<V> c = _root; c != null; c = c.Next)
                yield return c.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
