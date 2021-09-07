using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Linq;

namespace String
{
    public class FixedLengthEncoding_Trie : ICompression
    {
        public void Compress(string sourceFile, string compressedFile)
        {
            HashSet<char> characters = GetAllCharacter(sourceFile);
            Node_H root = BuildTrie(characters);
            string[] code = BuildCode(root);
            using (BinaryStdIn input = new BinaryStdIn(sourceFile))
            using (BinaryStdOut output = new BinaryStdOut(compressedFile))
            {
                output.Write((byte)code.Count(x => x != null));
                WriteTrie(output, root);
                output.Write(input.Length);
                WriteContent(input, output, code);
            }
        }

        public void Expand(string compressedFile, string expandedFile)
        {
            using (BinaryStdIn input = new BinaryStdIn(compressedFile))
            using (BinaryStdOut output = new BinaryStdOut(expandedFile))
            {
                Node_H root = ReadTrie(input);
                long size = input.ReadLong();
                Readcontents(input, output, root, size);
            }
        }

        protected HashSet<char> GetAllCharacter(string sourceFile)
        {
            HashSet<char> character = new HashSet<char>();
            using (BinaryStdIn input = new BinaryStdIn(sourceFile))
                while (!input.IsEmpty())
                    character.Add(input.ReadChar(8));
            return character;
        }

        protected Node_H BuildTrie(HashSet<char> characters)
        {
            Queue<Node_H> currentLevel = new Queue<Node_H>();
            foreach (char c in characters)
                currentLevel.Enqueue(new Node_H(c, 0, null, null));

            Queue<Node_H> upperLevel = new Queue<Node_H>();
            while (currentLevel.Count > 1)
            {
                while (currentLevel.Count > 0)
                    if (currentLevel.Count == 1)
                        upperLevel.Enqueue(new Node_H('\0', 0, currentLevel.Dequeue(), null));
                    else
                        upperLevel.Enqueue(new Node_H('\0', 0, currentLevel.Dequeue(), currentLevel.Dequeue()));
                currentLevel = upperLevel;
                upperLevel = new Queue<Node_H>();
            }
            return currentLevel.Dequeue();
        }

        protected string[] BuildCode(Node_H root)
        {
            string[] code = new string[256];
            BuildCode(root, "", code);
            return code;
        }

        protected void BuildCode(Node_H node, string text, string[] code)
        {
            if (!node.Key.Equals('\0'))
            {
                code[node.Key] = text;
                return;
            }

            if (node.Left != null)
                BuildCode(node.Left, text + "0", code);
            if (node.Right != null)
                BuildCode(node.Right, text + "1", code);
        }

        protected void WriteTrie(BinaryStdOut output, Node_H node)
        {
            if (node.Key.Equals('\0'))
            {
                output.Write(false);
                if (node.Left != null)
                    WriteTrie(output, node.Left);
                if (node.Right != null)
                    WriteTrie(output, node.Right);
            }
            else
            {
                output.Write(true);
                output.Write(node.Key, 8);
            }
        }

        protected void WriteContent(BinaryStdIn input, BinaryStdOut output, string[] code)
        {
            long size = input.Length;
            for (; size > 0; size--)
                foreach (char c in code[input.ReadChar(8)])
                    output.Write(c.Equals('1'));
        }

        protected Node_H ReadTrie(BinaryStdIn input)
        {
            byte count = input.ReadByte(8);
            return ReadTrie(input, ref count);
        }

        protected Node_H ReadTrie(BinaryStdIn input, ref byte count)
        {
            if (count == 0)
                return null;

            if (input.ReadBit())
            {
                count--;
                return new Node_H(input.ReadChar(8), 0, null, null);
            }
            else
                return new Node_H('\0', 0, ReadTrie(input, ref count), ReadTrie(input, ref count));
        }

        protected void Readcontents(BinaryStdIn input, BinaryStdOut output, Node_H root, long size)
        {
            for (; size > 0; size--)
            {
                Node_H current = root;
                while (current.Key.Equals('\0'))
                    if (input.ReadBit())
                        current = current.Right;
                    else
                        current = current.Left;
                output.Write(current.Key, 8);
            }
        }
    }
}
