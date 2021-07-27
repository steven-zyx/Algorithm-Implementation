using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Searching
{
    public class HeapsAlgorithm<T>
    {
        private int _limit;
        private Action<T[]> _output;
        private T[] _source;

        public HeapsAlgorithm(T[] source, Action<T[]> output, int limit)
        {
            _source = source;
            _output = output;
            _limit = limit;
        }

        public void Generate()
        {
            Generate(_source.Length);
        }

        private bool Generate(int k)
        {
            if (k == 1)
            {
                _output(_source);
                return --_limit == 0;
            }
            else
            {
                if (Generate(k - 1)) return true;
                for (int i = 0; i < k - 1; i++)
                {
                    if (k % 2 == 0)
                        Swap(i, k - 1);
                    else
                        Swap(0, k - 1);
                    if (Generate(k - 1)) return true;
                }
                return false;
            }
        }

        private void Swap(int a, int b)
        {
            T temp = _source[a];
            _source[a] = _source[b];
            _source[b] = temp;
        }
    }


    public class HashAttack
    {
        private int _length;
        private int _count;
        private Action<string> _output;
        private Queue<string> textList;
        private string[] _endChar = { "9", "X", "w" };

        public HashAttack(int length, int count, Action<string> output)
        {
            _length = length;
            _count = count;
            _output = output;
            textList = new Queue<string>(new string[] { "aw", "bX", "c9" });
        }

        public IEnumerable<string> GenerateText()
        {
            for (int n = 3; n <= _length; n++)
            {
                Queue<string> tempQueue = new Queue<string>();
                while (textList.Count > 0)
                {
                    char[] text = textList.Dequeue().ToCharArray();
                    for (int i = 0; i < 3; i++)
                    {
                        int l = text.Length;
                        text[l - 1] = (char)(text[l - 1] - 1);
                        tempQueue.Enqueue(new string(text) + _endChar[i]);

                        if (tempQueue.Count == _count)
                        {
                            textList.Clear();
                            break;
                        }
                    }
                }
                textList = tempQueue;
            }
            return textList;
        }
    }
}
