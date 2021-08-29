using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace String
{
    public class BinaryStdIn : IDisposable
    {
        protected FileStream _fs;
        protected BitArray _bits;
        protected const int BUFFER_SIZE = 4096;
        protected byte[] _buffer = new byte[BUFFER_SIZE];


        public BinaryStdIn(string fileName)
        {
            _fs = File.OpenRead(fileName);
            _bits = new BitArray(0);
            ContinueRead();
        }

        public void Close() => _fs.Close();

        protected byte[] Read(int digit)
        {
            if (digit > _bits.Length)
                ContinueRead();

            BitArray value = new BitArray(digit);
            for (int i = 0; i < digit; i++)
                value[i] = _bits[i];

            _bits.RightShift(digit);
            _bits.Length -= digit;

            byte[] content = new byte[8];
            value.CopyTo(content, 0);
            return content;
        }

        protected void ContinueRead()
        {
            int count = _fs.Read(_buffer, 0, _buffer.Length);
            if (count < BUFFER_SIZE)
            {
                byte[] content = new byte[count];
                Array.Copy(_buffer, content, count);
                _buffer = content;
            }

            BitArray augment = new BitArray(_buffer);
            augment.Length += _bits.Length;
            augment.LeftShift(_bits.Length);
            for (int i = 0; i < _bits.Length; i++)
                augment[i] = _bits[i];
            _bits = augment;
        }

        public bool ReadBit()
        {
            if (_bits.Length == 0)
                ContinueRead();

            bool result = _bits[0];
            _bits.RightShift(1);
            _bits.Length--;

            return result;
        }

        public char ReadChar(int digit = 16) => BitConverter.ToChar(Read(digit), 0);

        public int ReadInt(int digit = 32) => BitConverter.ToInt32(Read(digit), 0);

        public long ReadLong(int digit = 64) => BitConverter.ToInt64(Read(digit), 0);

        public bool IsEmpty()
        {
            if (_bits.Length == 0)
                ContinueRead();
            return _bits.Length == 0;
        }

        public long Length => _fs.Length;

        public void Dispose() => Close();
    }
}
