using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace String
{
    public class BinaryStdOut : IDisposable
    {
        protected BitArray _bits;
        protected string _fileName;
        protected FileStream _fs;
        protected const int BUFFER_LENGTH = 4096;
        protected const int BUFFER_LENGTH_BIT = BUFFER_LENGTH * 8;

        public BinaryStdOut(string fileName)
        {
            _bits = new BitArray(0);
            _fs = File.OpenWrite(fileName);
        }

        public void Write(long value, int digit = 64) => Write(BitConverter.GetBytes(value), digit);

        public void Write(int value, int digit = 32) => Write(BitConverter.GetBytes(value), digit);

        protected void Write(byte[] value, int digit)
        {
            int startIndex = _bits.Length;
            _bits.Length += digit;

            BitArray bitValue = new BitArray(value);
            for (int i = 0; i < digit; i++)
                _bits[startIndex + i] = bitValue[i];

            if (_bits.Length >= BUFFER_LENGTH_BIT)
            {
                byte[] content = new byte[BUFFER_LENGTH + 12];
                _bits.CopyTo(content, 0);
                _fs.Write(content, 0, BUFFER_LENGTH);

                _bits.RightShift(BUFFER_LENGTH_BIT);
                _bits.Length = _bits.Length - BUFFER_LENGTH_BIT;
            }
        }

        public void Close()
        {
            int byteLength = _bits.Length / 8;
            if (_bits.Length % 8 != 0)
                byteLength++;

            byte[] content = new byte[byteLength];
            _bits.CopyTo(content, 0);
            _fs.Write(content, 0, content.Length);
            _fs.Close();
        }

        public void Dispose() => Close();
    }
}
