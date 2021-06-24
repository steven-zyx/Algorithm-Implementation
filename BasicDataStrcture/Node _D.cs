using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Node_D<T>
    {
        public T Value;
        public Node_D<T> Previous;
        public Node_D<T> Next;

        public Node_D(T t)
        {
            Value = t;
        }
    }
}
