using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace String
{
    public class BinaryStdIn
    {
        protected string _fileName;
        protected BitArray _bits;

        public BinaryStdIn(string fileName)
        {
            _fileName = fileName;
            byte[] content = File.ReadAllBytes(_fileName);
            _bits = new BitArray(content);
        }

        public V Read<V>(int digit)
        {
            BitArray value = new BitArray(digit);
            for (int i = 0; i < digit; i++)
                value[i] = _bits[i];

            _bits.RightShift(digit);
            _bits.Length -= digit;

            V[] number = new V[1];
            value.CopyTo(number, 0);
            return number[0];
        }
    }
}
