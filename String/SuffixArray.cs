using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using String;
using Searching;

namespace String
{
    public class SuffixArray
    {
        public int Length { get; }
        protected BinarySearchSET<string> _client;

        public SuffixArray(string text)
        {
            Length = text.Length;
            string[] suffixs = new string[Length];
            for (int i = 0; i < Length; i++)
                suffixs[i] = text.Substring(i);

            ThreeWayStringQuickSort sortClient = new ThreeWayStringQuickSort();
            sortClient.Sort(suffixs);
            _client = new BinarySearchSET<string>(suffixs);
        }

        public int Index(int i) => Length - _client.Select(i).Length;

        public int LCP(int i)
        {
            string a = _client.Select(i - 1), b = _client.Select(i);
            for (int n = 0; n < a.Length; n++)
                if (a[n] != b[n])
                    return n;
            return a.Length;
        }

        public string Select(int i) => _client.Select(i);

        public int Rank(string key) => _client.Rank(key);
    }
}
