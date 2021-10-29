using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class LongestPath_NativeCycle : LongestPath4WeightedDigraph
    {
        public LongestPath_NativeCycle(EdgeWeightedDigraph g, int s)
        {
            EdgeWeightedDigraph nagetiveG = Negate(g);
            _client = new BellmanFord(nagetiveG, s);
        }

        public bool HasCycle => (_client as BellmanFord).HasCycle;

        public IEnumerable<int> Cycle => (_client as BellmanFord).Cycle;
    }
}
