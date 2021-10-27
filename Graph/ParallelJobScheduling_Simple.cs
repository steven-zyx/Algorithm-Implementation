using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ParallelJobScheduling_Simple : ParallelJobScheduling
    {
        public ParallelJobScheduling_Simple((int job, int duration, int[] followings)[] problem)
        {
            var result = Construct(problem, null);
            _client = new LongestPath_DAG(result.g, result.s);
        }
    }
}
