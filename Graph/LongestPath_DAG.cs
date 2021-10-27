using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LongestPath_DAG : LongestPath4WeightedDigraph
    {
        public LongestPath_DAG(EdgeWeightedDigraph g, int s)
        {
            EdgeWeightedDigraph nagetiveG = Negate(g);
            _client = new ShortestPath_DAG(nagetiveG, s);
        }

        public bool IsDAG => (_client as ShortestPath_DAG).IsDAG;
    }
}
