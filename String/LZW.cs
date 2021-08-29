﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace String
{
    public class LZW : ICompression
    {
        protected readonly int R = 256;
        protected readonly int L = 12;
        protected readonly int C = 4096;
        protected const int BUFFER_SIZE = 4096;

        public void Compress(string sourceFile, string compressedFile)
        {
            TST<short> code = new TST<short>();
            short index = 0;
            for (; index < R; index++)
                code.Put(((char)index).ToString(), index);
            index++;

            using (FileStream fs = File.OpenRead(sourceFile))
            using (BinaryStdOut output = new BinaryStdOut(compressedFile))
            {
                string text = ReadString(fs, BUFFER_SIZE);
                while (true)
                {
                    string prefix = code.LongestPrefixOf(text);
                    if (text.Length == prefix.Length)
                    {
                        string newText = ReadString(fs, BUFFER_SIZE);
                        if (newText.Length == 0)
                        {
                            output.Write(code.Get(prefix), L);
                            output.Write(R, L);
                            break;
                        }
                        else
                        {
                            text += newText;
                            continue;
                        }
                    }
                    else
                    {
                        output.Write(code.Get(prefix), L);
                        if (index < C)
                            code.Put(text.Substring(0, prefix.Length + 1), index++);
                        text = text.Substring(prefix.Length);
                    }
                }
            }
        }

        protected string ReadString(FileStream fs, int length)
        {
            byte[] content = new byte[length];
            int count = fs.Read(content, 0, length);
            return Encoding.ASCII.GetString(content, 0, count);
        }

        public void Expand(string compressedFile, string expandedFile)
        {
            string[] code = new string[C];
            int index = 0;
            for (; index < R; index++)
                code[index] = ((char)index).ToString();
            code[index++] = "\0";

            using (BinaryStdIn input = new BinaryStdIn(compressedFile))
            using (BinaryStdOut output = new BinaryStdOut(expandedFile))
            {
                string val = code[input.ReadChar(12)];
                while (true)
                {
                    foreach (char c in val)
                        output.Write(c, 8);

                    int next = input.ReadChar(12);
                    if (next == R)
                        break;

                    if (index < C)
                        if (next >= index)
                            code[index++] = val + val[0];
                        else
                            code[index++] = val + code[next][0];
                    val = code[next];
                }
            }
        }
    }
}
