using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class DequeueSort
    {
        public void Sort(Queue<int> source, int min, int max)
        {
            int top1 = source.Dequeue();
            bool sorted = false;
            while (true)
            {
                int top2 = source.Dequeue();
                if (top1 == max && top2 == min)
                {
                    source.Enqueue(top1);
                    top1 = top2;
                }
                else if (top1 == min && top2 == max)
                {
                    source.Enqueue(top2);
                    sorted = false;
                }
                else if (top1 < top2)
                {
                    source.Enqueue(top1);
                    top1 = top2;
                }
                else
                {
                    source.Enqueue(top2);
                    sorted = false;
                }

                if (top1 == max)
                {
                    if (sorted)
                    {
                        source.Enqueue(top1);
                        break;
                    }
                    else
                        sorted = true;
                }
            }
        }
    }
}
