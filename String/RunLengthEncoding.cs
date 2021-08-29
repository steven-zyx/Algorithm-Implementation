using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace String
{
    public class RunLengthEncoding : ICompression
    {
        private const int BUFFER_SIZE = 4096;
        private const int MAX_RUN_LENGTH = 255;

        public void Compress(string source, string compressed)
        {
            using (BinaryStdIn input = new BinaryStdIn(source))
            using (BinaryStdOut output = new BinaryStdOut(compressed))
            {
                bool currentBit = false;
                int runLength = 0;
                while (!input.IsEmpty())
                {
                    if (currentBit == input.ReadBit())
                        runLength++;
                    else
                    {
                        output.Write(runLength, 8);
                        runLength = 1;
                        currentBit = !currentBit;
                    }

                    if (runLength > MAX_RUN_LENGTH)
                    {
                        output.Write(MAX_RUN_LENGTH, 8);
                        output.Write(0, 8);
                        runLength = 1;
                    }
                }
                output.Write(runLength, 8);
            }
        }

        public void Expand(string compressed, string expanded)
        {
            using (BinaryStdIn input = new BinaryStdIn(compressed))
            using (BinaryStdOut output = new BinaryStdOut(expanded))
            {
                bool currentBit = false;
                while (!input.IsEmpty())
                {
                    int cnt = input.ReadInt(8);
                    for (int i = 0; i < cnt; i++)
                        output.Write(currentBit);
                    currentBit = !currentBit;
                }
            }
        }
    }
}
