using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Node<T>
    {
        public Node<T> Next;
        public T Value;

        public Node(T t)
        {
            Value = t;
        }
    }
}
