using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class Node_H : IComparable
    {
        public char Key { get; }

        public int Count { get; }

        public Node_H Left { get; }

        public Node_H Right { get; }

        public Node_H(char key, int count, Node_H left, Node_H right)
        {
            Key = key;
            Count = count;
            Left = left;
            Right = right;
        }

        public int CompareTo(object obj) => Count.CompareTo((obj as Node_H).Count);
    }
}
