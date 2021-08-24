using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace String
{
    public class BinaryStdIn : IDisposable
    {
        protected FileStream _fs;
        protected BitArray _bits;
        protected const int BUFFER_SIZE = 4096;

        public BinaryStdIn(string fileName)
        {
            _fs = File.OpenRead(fileName);
            _bits = Read();
        }

        public void Close() => _fs.Close();

        protected byte[] Read(int digit)
        {
            if (digit > _bits.Length)
            {
                BitArray augment = Read();
                augment.LeftShift(_bits.Length);
                for (int i = 0; i < _bits.Length; i++)
                    augment[i] = _bits[i];
                _bits = augment;
            }

            BitArray value = new BitArray(digit);
            for (int i = 0; i < digit; i++)
                value[i] = _bits[i];

            _bits.RightShift(digit);
            _bits.Length -= digit;

            byte[] content = new byte[8];
            value.CopyTo(content, 0);
            return content;
        }

        protected BitArray Read()
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            _fs.Read(buffer, 0, BUFFER_SIZE);
            return new BitArray(buffer);
        }

        public int ReadInt(int digit = 32) => BitConverter.ToInt32(Read(digit), 0);

        public long ReadLong(int digit = 64) => BitConverter.ToInt64(Read(digit), 0);

        public void Dispose() => Close();
    }
}
