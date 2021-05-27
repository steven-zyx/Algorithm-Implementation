using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class BottomUpQueueMergeSort
    {
        private MergeSortedQueues _client = new MergeSortedQueues();

        public Queue<int> Sort(int[] source)
        {
            Queue<Queue<int>> result = new Queue<Queue<int>>(source.Length);
            foreach (int n in source)
            {
                Queue<int> Q = new Queue<int>();
                Q.Enqueue(n);
                result.Enqueue(Q);
            }
            while (result.Count > 1)
            {
                result.Enqueue(_client.Sort(result.Dequeue(), result.Dequeue()));
            }
            return result.Dequeue();
        }
    }
}
