using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public interface IDeque<T>
    {
        void PushLeft(T t);

        void PushRight(T t);

        T PopLeft();

        T PopRight();

        bool IsEmpty { get; }

        int Size { get; }
    }
}
