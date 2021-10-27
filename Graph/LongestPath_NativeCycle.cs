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
            _client = new ShortestPath_NagetiveCycle(nagetiveG, s);
        }

        public bool HasCycle => (_client as ShortestPath_NagetiveCycle).HasCycle;

        public IEnumerable<int> Cycle => (_client as ShortestPath_NagetiveCycle).Cycle;
    }
}
