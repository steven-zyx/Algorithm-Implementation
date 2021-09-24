using System;
using System.Collections.Generic;
using System.Text;
using BasicDataStrcture;
using System.Linq;

namespace String
{
    public class BoyerMoore : SubStringSearch
    {
        protected const int R = 256;
        protected string _pattern;
        protected int[] _right;

        public BoyerMoore(string pattern) : base(pattern)
        {
            _pattern = pattern;
            _right = new int[R];
            for (int i = 0; i < R; i++)
                _right[i] = -1;
            for (int j = 0; j < M; j++)
                _right[pattern[j]] = j;
        }

        public override int Search(string text)
        {
            int N = text.Length;
            int skip;
            for (int i = 0; i <= N - M; i += skip)
            {
                skip = 0;
                for (int j = M - 1; j >= 0; j--)
                    if (!text[i + j].Equals(_pattern[j]))
                    {
                        skip = j - _right[text[i + j]];
                        if (skip < 1)
                            skip = 1;
                        break;
                    }
                if (skip == 0)
                    return i;
            }
            return N;
        }

        public override int Search(BinaryStdIn input)
        {
            Deque_N<char> buffer = new Deque_N<char>();
            for (int i = 0; i < M; i++)
                buffer.PushRight(input.ReadChar(8));

            int skip = 0;
            int position = 0;
            do
            {
                int index = M - 1;
                foreach (char c in buffer.FromRightToLeft())
                    if (!_pattern[index].Equals(c))
                    {
                        skip = index - _right[c];
                        if (skip < 1)
                            skip = 1;
                        break;
                    }
                    else
                        index--;
                if (skip == 0)
                    return position;

                for (; skip > 0 && !input.IsEmpty(); skip--)
                {
                    buffer.PopLeft();
                    buffer.PushRight(input.ReadChar(8));
                    position++;
                }
            } while (skip == 0);
            return position + M;
        }

        public override IEnumerable<int> FindAll(string text)
        {
            int N = text.Length;
            int skip;
            for (int i = 0; i <= N - M; i += skip)
            {
                skip = 0;
                for (int j = M - 1; j >= 0; j--)
                    if (!text[i + j].Equals(_pattern[j]))
                    {
                        skip = j - _right[text[i + j]];
                        if (skip < 1)
                            skip = 1;
                        break;
                    }
                if (skip == 0)
                {
                    yield return i;
                    i++;
                }
            }
        }
    }
}
