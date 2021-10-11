using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class DirectedEdge
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
    }
}
