using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace String
{
    public class BinaryStdIn : IDisposable
    {
        protected string _fileName;
        protected BitArray _bits;

        public BinaryStdIn(string fileName)
        {
            _fileName = fileName;
            byte[] content = File.ReadAllBytes(_fileName);
            _bits = new BitArray(content);
        }

        protected byte[] Read(int digit)
        {
            BitArray value = new BitArray(digit);
            for (int i = 0; i < digit; i++)
                value[i] = _bits[i];

            _bits.RightShift(digit);
            _bits.Length -= digit;

            byte[] content = new byte[8];
            value.CopyTo(content, 0);
            return content;
        }

        public int ReadInt(int digit = 32) => BitConverter.ToInt32(Read(digit), 0);

        public long ReadLong(int digit = 64) => BitConverter.ToInt64(Read(digit), 0);

        public void Dispose() { }
    }
}
