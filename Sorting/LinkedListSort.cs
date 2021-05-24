using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace Sorting
{
    public class LinkedListSort
    {
        public Node<int> Sort(Node<int> start)
        {
            var originalP = new Node<int>(int.MinValue);
            originalP.Next = start;

            int subArrayCount = 1;
            int mergeCount = 0;

            do
            {
                mergeCount = 0;
                Node<int> p = originalP;
                Node<int> right;
                while (p.Next != null)
                {
                    right = p.Next;
                    for (int i = 0; i < subArrayCount && right != null; i++)
                        right = right.Next;

                    mergeCount++;
                    if (right == null)
                        break;
                    else
                        p = Merge(p, p.Next, right, subArrayCount);
                }
                subArrayCount *= 2;
            }
            while (mergeCount > 1);

            return originalP.Next;
        }


        public Node<int> Merge(Node<int> p, Node<int> left, Node<int> right, int subArrayCount)
        {
            int leftTaken = 0;
            int rightTaken = 0;

            while (true)
            {
                if (leftTaken >= subArrayCount && (rightTaken >= subArrayCount || right == null))
                {
                    p.Next = right;
                    return p;
                }
                else if (leftTaken >= subArrayCount)
                {
                    rightTaken++;
                    p.Next = right;
                    right = right.Next;
                }
                else if (rightTaken >= subArrayCount || right == null)
                {
                    leftTaken++;
                    p.Next = left;
                    left = left.Next;
                }
                else if (left.Value < right.Value)
                {
                    leftTaken++;
                    p.Next = left;
                    left = left.Next;
                }
                else
                {
                    rightTaken++;
                    p.Next = right;
                    right = right.Next;
                }
                p = p.Next;
            }
        }
    }
}
