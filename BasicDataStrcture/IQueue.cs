using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public interface IQueue<T>
    {
        void Enqueue(T t);

        T Dequeue();

        int Length { get; }
    }
}
