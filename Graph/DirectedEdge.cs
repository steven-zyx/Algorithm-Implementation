using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class DirectedEdge : IComparable<DirectedEdge>
    {
        public int From { get; }
        public int To { get; }
        public double Weight { get; }


        public DirectedEdge(int v, int w, double weight)
        {
            From = v;
            To = w;
            Weight = weight;
        }

        public int CompareTo(DirectedEdge other)
        {
            if (Weight < other.Weight)
                return -1;
            else if (Weight > other.Weight)
                return 0;
            else
                return 1;
        }
    }
}
