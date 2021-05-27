using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class MergeSortedQueues
    {
        public Queue<int> Sort(Queue<int> left, Queue<int> right)
        {
            Queue<int> result = new Queue<int>(left.Count + right.Count);
            while (true)
            {
                if (right.Count == 0)
                {
                    while (left.Count > 0)
                        result.Enqueue(left.Dequeue());
                    break;
                }
                else if (left.Count == 0)
                {
                    while (right.Count > 0)
                        result.Enqueue(right.Dequeue());
                    break;
                }
                else if (left.Peek() < right.Peek())
                {
                    result.Enqueue(left.Dequeue());
                }
                else
                {
                    result.Enqueue(right.Dequeue());
                }
            }
            return result;
        }
    }
}
