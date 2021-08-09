using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public class ThreeWayStringQuickSort4LinkedList
    {
        public void Sort(Node_D<string> start, Node_D<string> end) =>
            Sort(start, end, 0);

        private void Sort(Node_D<string> lo, Node_D<string> hi, int digit)
        {
            if (ReferenceEquals(lo, hi) || hi == null || ReferenceEquals(lo, hi.Next) || lo == null)
                return;

            Node_D<string> lt = lo, gt = hi, i = lo.Next;
            int v = CharAt(lo.Value, digit);

            while (!ReferenceEquals(i, gt.Next) && i != null)
            {
                int result = CharAt(i.Value, digit).CompareTo(v);
                if (result < 0)
                {
                    Exchange(i, lt);
                    i = i.Next;
                    lt = lt.Next;
                }
                else if (result > 0)
                {
                    Exchange(i, gt);
                    gt = gt.Previous;
                }
                else
                    i = i.Next;
            }

            Sort(lo, lt.Previous, digit);
            if (v >= 0)
                Sort(lt, gt, digit + 1);
            Sort(gt.Next, hi, digit);
        }

        private int CharAt(string text, int digit)
        {
            if (digit >= text.Length)
                return -1;
            else
                return text[digit];
        }

        private void Exchange(Node_D<string> a, Node_D<string> b)
        {
            string temp = a.Value;
            a.Value = b.Value;
            b.Value = temp;
        }
    }
}
