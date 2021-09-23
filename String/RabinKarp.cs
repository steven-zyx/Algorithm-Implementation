using System;
using System.Collections.Generic;
using System.Text;

namespace String
{
    public class RabinKarp : SubStringSearch
    {
        protected const int R = 255;
        protected readonly ulong Q = (ulong)Math.Pow(2, 56) - 5;
        protected ulong RM;
        protected ulong _hash;

        public RabinKarp(string pattern) : base(pattern)
        {
            _hash = ComputeHash(pattern);
            RM = R;
            for (int i = 2; i < M; i++)
                RM = RM * R % Q;
        }

        public override int Search(string text)
        {
            int N = text.Length;
            ulong fp = ComputeHash(text.Substring(0, M));
            if (fp == _hash)
                return 0;
            for (int i = M; i < N; i++)
            {
                fp = (fp + Q - text[i - M] * RM % Q) % Q;
                fp = (fp * R + text[i]) % Q;
                if (fp == _hash)
                    return i - M + 1;
            }
            return N;
        }


        protected ulong ComputeHash(string pattern)
        {
            ulong h = 0;
            foreach (char c in pattern)
                h = (h * R + c) % Q;
            return h;
        }
    }
}
