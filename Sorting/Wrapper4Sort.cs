using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class Wrapper4Sort<T> : IComparable where T : IComparable
    {
        private T _obj;
        private int _index;

        public Wrapper4Sort(T obj, int index)
        {
            _obj = obj;
            _index = index;
        }

        public T Obj => _obj;

        public int CompareTo(object obj)
        {
            Wrapper4Sort<T> otherW = obj as Wrapper4Sort<T>;
            int result = _obj.CompareTo(otherW._obj);
            if (result == 0)
                return _index - otherW._index;
            else
                return result;
        }

        public static Wrapper4Sort<T>[] Wrap(T[] source)
        {
            Wrapper4Sort<T>[] result = new Wrapper4Sort<T>[source.Length];
            for (int i = 0; i < source.Length; i++)
                result[i] = new Wrapper4Sort<T>(source[i], i);
            return result;
        }
    }
}
