using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Linq;

namespace String
{
    public class QueueSort
    {
        private Alphabet _a;

        public void Sort(string[] source, Alphabet a)
        {
            _a = a;

            IQueue<string>[] bucket = new IQueue<string>[_a.R + 1];
            foreach (string text in source)
            {
                int index = CharAt(text, 0) + 1;
                if (bucket[index] == null)
                    bucket[index] = new MinPQ4QueueSort(1);
                bucket[index].Enqueue(text);
            }

            int i = 0;
            foreach (IQueue<string> queue in bucket.Where(x => x != null))
                while (queue.Length > 0)
                    source[i++] = queue.Dequeue();
        }

        private int CharAt(string text, int d)
        {
            if (d >= text.Length)
                return -1;
            else
                return _a.ToIndex(text[d]);
        }
    }

    public class MinPQ4QueueSort : IQueue<string>
    {
        private int _count;
        private string[] _heap;
        private int _digit;

        public MinPQ4QueueSort(int digit)
        {
            _count = 0;
            _heap = new string[16];
            _digit = digit;
        }

        public string Dequeue()
        {
            string value = _heap[1];
            Exchange(1, _count--);
            Sink(1);

            if (_count < _heap.Length / 4)
                Resize(_heap.Length / 2);

            return value;
        }

        public void Enqueue(string t)
        {
            _heap[++_count] = t;
            Swim(_count);

            if (_count + 1 >= _heap.Length)
                Resize(_heap.Length * 2);
        }

        private void Swim(int i)
        {
            while (i > 1 && Less(i, i / 2))
            {
                Exchange(i, i / 2);
                i = i / 2;
            }
        }

        private void Sink(int i)
        {
            while (i * 2 <= _count)
            {
                int j = i * 2;
                if (j + 1 <= _count && Less(j + 1, j))
                    j++;

                if (Less(j, i))
                {
                    Exchange(i, j);
                    i = j;
                }
                else
                    break;
            }
        }

        private bool Less(int a, int b)
        {
            if (_digit > _heap[a].Length)
                return false;
            else
                return _heap[a].Substring(_digit).CompareTo(_heap[b].Substring(_digit)) < 0;
        }

        private void Exchange(int a, int b)
        {
            string temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }

        private void Resize(int size)
        {
            string[] newHeap = new string[size];
            Array.Copy(_heap, newHeap, _count + 1);
            _heap = newHeap;
        }

        public int Length => _count;
    }
}

