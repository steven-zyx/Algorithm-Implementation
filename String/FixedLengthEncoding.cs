using System;
using System.IO;

namespace String
{
    public class FixedLengthEncoding
    {
        public static void Compress(string source, string compressedFile, Alphabet alphabet)
        {
            using (FileStream fs = File.OpenRead(source))
            using (StreamReader reader = new StreamReader(fs))
            using (BinaryStdOut output = new BinaryStdOut(compressedFile))
            {
                output.Write(fs.Length);

                int bufferLength = 2048;
                int byteRead = 2048;
                char[] buffer = new char[bufferLength];
                int digit = alphabet.LgR();
                while (byteRead == bufferLength)
                {
                    byteRead = reader.Read(buffer, 0, bufferLength);
                    for (int i = 0; i < byteRead; i++)
                        output.Write(alphabet.ToIndex(buffer[i]), digit);
                }
            }
        }

        public static void Expand(string compressedFile, string source, Alphabet alphabet)
        {
            using (BinaryStdIn input = new BinaryStdIn(compressedFile))
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(source)))
            {
                long length = input.ReadLong();
                int digit = alphabet.LgR();
                for (int i = 0; i < length; i++)
                    writer.Write(alphabet.ToChar(input.ReadInt(digit)));
            }
        }
    }
}
