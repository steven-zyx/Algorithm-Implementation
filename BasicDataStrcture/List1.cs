using System;

namespace BasicDataStrcture
{
    public class List1<T>
    {
        private T[] _data;
        private int _current;

        public List1()
        {
            _data = new T[4];
        }

        public void Add(T t)
        {
            if (_current == _data.Length)
                Resize();

            _data[_current] = t;
            _current++;
        }

        public int Count
        {
            get { return _current; }
        }

        public T this[int i]
        {
            get
            {
                if (i >= _current)
                    throw new Exception("index out of range");
                else
                    return _data[i];
            }
            set
            {
                if (i >= _current)
                    throw new Exception("index out of range");
                else
                    _data[i] = value;
            }
        }

        public void Resize()
        {
            T[] newData = new T[_data.Length * 2];
            _data.CopyTo(newData, 0);
            _data = newData;
        }
    }
}
