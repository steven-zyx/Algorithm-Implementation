using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace String
{
    public class BinaryStdOut
    {
        protected BitArray _bits;
        protected int _bitIndex;
        protected string _fileName;

        public BinaryStdOut(string fileName)
        {
            _bitIndex = 0;
            _bits = new BitArray(8);
            _fileName = fileName;
        }

        public void Write(int number, int digit)
        {
            BitVector32 bits = new BitVector32(number);
            for (int i = 0; i < digit; i++)
                _bits[_bitIndex++] = bits[i];
        }

        public void Close()
        {
            int length = _bitIndex / 8;
            if (length % 8 != 0)
                length++;

            byte[] content = new byte[length];
            _bits.CopyTo(content, 0);

            File.WriteAllBytes(_fileName, content);
        }
    }
}
