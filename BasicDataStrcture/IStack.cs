using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public interface IStack<T>
    {
        void Push(T t);

        T Pop();

        int Length { get; }
    }
}
