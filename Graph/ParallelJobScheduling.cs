using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ParallelJobScheduling
    {
        protected LongestPath_DAG _client;

        public ParallelJobScheduling((int job, int duration, int[] followings)[] problem)
        {
            int n = problem.Length;
            int s = 2 * n, t = 2 * n + 1;
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(2 * n + 2);

            foreach (var triple in problem)
            {
                int v = triple.job, w = triple.job + n;
                g.AddEdge(s, v, 0);
                g.AddEdge(v, w, triple.duration);
                g.AddEdge(w, t, 0);
                if (triple.followings != null)
                    foreach (int u in triple.followings)
                        g.AddEdge(w, u, 0);
            }

            _client = new LongestPath_DAG(g, s);
        }

        public int StartTime(int job)
        {
            double dist = _client.DistTo(job);
            return (int)Math.Round(dist, 10);
        }
    }
}
