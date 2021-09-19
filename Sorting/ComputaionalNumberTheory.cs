using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class PolyNomial : IComparable<PolyNomial>
    {
        public long Sum { get; set; }
        public int A { get; set; }
        public int B { get; set; }

        public PolyNomial(int a, int b)
        {
            A = a;
            B = b;
            double temp = Math.Pow(a, 3) + Math.Pow(b, 3);
            Sum = (long)temp;
        }

        public int CompareTo(PolyNomial obj)
        {
            long result = Sum - obj.Sum;
            if (result > 0)
                return 1;
            else if (result == 0)
                return 0;
            else
                return -1;
        }

        public bool IsDifferent(PolyNomial obj)
        {
            return A != obj.A && A != obj.B && B != obj.A && B != obj.B && A != B && obj.A != obj.B;
        }
    }

    public class ComputaionalNumberTheory
    {
        private Dictionary<long, List<PolyNomial>> _result = new Dictionary<long, List<PolyNomial>>();
        private int _N;
        private MinPQ<PolyNomial> _pq = new MinPQ<PolyNomial>();


        public ComputaionalNumberTheory(int n)
        {
            _N = n;
            for (int i = 0; i <= _N; i++)
            {
                _pq.Insert(new PolyNomial(i, 0));
            }
        }

        public void Calculate()
        {
            while (!_pq.IsEmpty)
            {
                PolyNomial item = _pq.DeleteMin();
                if (item.B < _N)
                {
                    _pq.Insert(new PolyNomial(item.A, item.B + 1));
                }
                if (!_result.ContainsKey(item.Sum) || _result[item.Sum] == null)
                    _result[item.Sum] = new List<PolyNomial>() { item };
                else
                    _result[item.Sum].Add(item);
            }
        }

        public void OutputPairs()
        {
            foreach (var pair in _result)
            {
                bool isEmpty = true;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"For sum:\t{pair.Key}");

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    for (int j = i + 1; j < pair.Value.Count; j++)
                    {
                        var l = pair.Value[i];
                        var r = pair.Value[j];
                        if (l.IsDifferent(r))
                        {
                            isEmpty = false;
                            sb.AppendLine($"{l.A}^3 + {l.B}^3 = {r.A}^3 + {r.B}^3");
                        }
                    }
                }
                if (!isEmpty)
                    Console.WriteLine(sb.ToString());
            }
        }
    }
}
