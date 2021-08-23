using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace String
{
    public class BinaryStdOut
    {
        protected BitArray _bits;
        protected string _fileName;

        public BinaryStdOut(string fileName)
        {
            _bits = new BitArray(0);
            _fileName = fileName;
        }

        public void Write(int value, int digit) => Write(BitConverter.GetBytes(value), digit);

        protected void Write(byte[] value, int digit)
        {
            int startIndex = _bits.Length;
            _bits.Length += digit;

            BitArray content = new BitArray(value);
            for (int i = 0; i < digit; i++)
                _bits[startIndex + i] = content[i];
        }

        public void Close()
        {
            int byteLength = _bits.Length / 8;
            if (_bits.Length % 8 != 0)
                byteLength++;

            byte[] content = new byte[byteLength];
            _bits.CopyTo(content, 0);

            File.WriteAllBytes(_fileName, content);
        }
    }
}
