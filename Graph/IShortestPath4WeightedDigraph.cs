using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public interface IShortestPath4WeightedDigraph
    {
        double[] DistTo { get; }

        bool HashPathTo(int v);

        IEnumerable<Edge> PathTo(int v);
    }
}
