using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ParallelJobScheduling_Deadlines : ParallelJobScheduling
    {
        public ParallelJobScheduling_Deadlines(
            (int job, int duration, int[] followings)[] problem,
            (int job, int time, int relativeTo)[] deadlines)
        {
            var graph = Construct(problem, deadlines);
            _client = new LongestPath_NativeCycle(graph.g, graph.s);
        }

        public bool HasCycle => (_client as LongestPath_NativeCycle).HasCycle;

        public IEnumerable<int> Cycle => (_client as LongestPath_NativeCycle).Cycle;
    }
}
