using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Node2<T>
    {
        public T Value;
        public Node2<T> Previous;
        public Node2<T> Next;

        public Node2(T t)
        {
            Value = t;
        }
    }
}
