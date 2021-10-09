using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Edge : IComparable<Edge>
    {
        protected int _v;
        protected int _w;

        public double Weight { get; }

        public Edge(int v, int w, double weight)
        {
            _v = v;
            _w = w;
            Weight = weight;
        }

        public int Either() => _v;

        public int Other(int v)
        {
            if (v == _v) return _w;
            else if (v == _w) return _v;
            else
                throw new InvalidOperationException();
        }

        public int CompareTo(Edge other)
        {
            if (Weight < other.Weight)
                return -1;
            else if (Weight > other.Weight)
                return 1;
            else
                return 0;
        }
    }
}
