using System;
using System.IO;

namespace String
{
    public class FixedLengthEncoding
    {
        public static void Compress(string source, string compressedFile, Alphabet alphabet)
        {
            using (BinaryStdIn input = new BinaryStdIn(source))
            using (BinaryStdOut output = new BinaryStdOut(compressedFile))
            {
                output.Write(input.Length);
                int digit = alphabet.LgR();
                while (!input.IsEmpty())
                    output.Write(alphabet.ToIndex(input.ReadChar()), digit);
            }
        }

        public static void Expand(string compressedFile, string source, Alphabet alphabet)
        {
            using (BinaryStdIn input = new BinaryStdIn(compressedFile))
            using (BinaryStdOut output = new BinaryStdOut(source))
            {
                long length = input.ReadLong();
                int digit = alphabet.LgR();
                for (int i = 0; i < length; i++)
                    output.Write(alphabet.ToChar(input.ReadInt(digit)));
            }
        }
    }
}
