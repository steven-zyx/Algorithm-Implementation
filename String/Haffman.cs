using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using Sorting;

namespace String
{
    public class Haffman : ICompression
    {
        public void Compress(string sourceFile, string compressedFile)
        {
            int[] frequency = ComputeFrequency(sourceFile);
            Node_H trie = BuildTrie(frequency);
            string[] code = new string[255];
            BuildCode(trie, "", code);

            using (BinaryStdIn input = new BinaryStdIn(sourceFile))
            using (BinaryStdOut output = new BinaryStdOut(compressedFile))
            {
                WriteTrie(trie, output);
                output.Write(trie.Count);
                while (!input.IsEmpty())
                {
                    string bits = code[input.ReadChar(8)];
                    foreach (char bit in bits)
                        output.Write(bit.Equals('1'));
                }
            }
        }

        private int[] ComputeFrequency(string sourceFile)
        {
            int[] frequency = new int[255];
            using (BinaryStdIn input = new BinaryStdIn(sourceFile))
                while (!input.IsEmpty())
                    frequency[input.ReadChar(8)]++;
            return frequency;
        }

        private Node_H BuildTrie(int[] frequency)
        {
            MinPQ<Node_H> nodeList = new MinPQ<Node_H>();
            for (int i = 0; i < frequency.Length; i++)
                if (frequency[i] > 0)
                    nodeList.Insert(new Node_H((char)i, frequency[i], null, null));

            while (nodeList.Size > 1)
            {
                Node_H left = nodeList.DeleteMin();
                Node_H right = nodeList.DeleteMin();
                nodeList.Insert(new Node_H((char)0, left.Count + right.Count, left, right));
            }
            return nodeList.DeleteMin();
        }

        private void BuildCode(Node_H node, string code, string[] st)
        {
            if (node.Key.Equals((char)0))
            {
                BuildCode(node.Left, code + '0', st);
                BuildCode(node.Right, code + '1', st);
            }
            else
                st[node.Key] = code;
        }

        private void WriteTrie(Node_H node, BinaryStdOut output)
        {
            if (node.Key.Equals((char)0))
            {
                output.Write(false);
                WriteTrie(node.Left, output);
                WriteTrie(node.Right, output);
            }
            else
            {
                output.Write(true);
                output.Write(node.Key, 8);
            }
        }

        public void Expand(string compressedFile, string expandedFile)
        {
            using (BinaryStdIn input = new BinaryStdIn(compressedFile))
            using (BinaryStdOut output = new BinaryStdOut(expandedFile))
            {
                Node_H trie = ReadTrie(input);
                int count = input.ReadInt();
                for (; count > 0; count--)
                {
                    Node_H node = trie;
                    while (node.Key.Equals((char)0))
                        node = input.ReadBit() ? node.Right : node.Left;
                    output.Write(node.Key, 8);
                }
            }
        }

        private Node_H ReadTrie(BinaryStdIn input)
        {
            if (input.ReadBit())
                return new Node_H(input.ReadChar(8), -1, null, null);
            else
                return new Node_H((char)0, -1, ReadTrie(input), ReadTrie(input));
        }
    }
}
