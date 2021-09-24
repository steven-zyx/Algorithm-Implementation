using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;

namespace String
{
    public class BruteSearch1 : SubStringSearch
    {
        protected string _pattern;

        public BruteSearch1(string pattern) : base(pattern)
            => _pattern = pattern;

        public override int Search(string text)
        {
            int N = text.Length;
            for (int i = 0; i <= N - M; i++)
            {
                int j = 0;
                for (; j < M; j++)
                    if (!text[i + j].Equals(_pattern[j]))
                        break;
                if (j == M)
                    return i;
            }
            return N;
        }

        public override int Search(BinaryStdIn input)
        {
            Queue_N<char> buffer = new Queue_N<char>();
            for (int i = 0; i < M - 1; i++)
                buffer.Enqueue(input.ReadChar(8));

            int position = 0;
            while (!input.IsEmpty())
            {
                buffer.Enqueue(input.ReadChar(8));

                int index = 0;
                foreach (char c in buffer)
                    if (!_pattern[index].Equals(c))
                        break;
                    else
                        index++;
                if (index == M)
                    return position;

                buffer.Dequeue();
                position++;
            }
            return position + M - 1;
        }

        public override IEnumerable<int> FindAll(string text)
        {
            int N = text.Length;
            for (int i = 0; i <= N - M; i++)
            {
                int j = 0;
                for (; j < M; j++)
                    if (!text[i + j].Equals(_pattern[j]))
                        break;
                if (j == M)
                    yield return i;
            }
        }
    }
}
