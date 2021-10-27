using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public abstract class ParallelJobScheduling
    {
        protected LongestPath4WeightedDigraph _client;

        protected (EdgeWeightedDigraph g, int s) Construct(
            (int job, int duration, int[] followings)[] problem,
            (int job, int time, int relativeTo)[] deadlines)
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

            if (deadlines != null)
                foreach (var deadline in deadlines)
                    g.AddEdge(deadline.job, deadline.relativeTo, -deadline.time);

            return (g, s);
        }

        public int StartTime(int job)
        {
            double dist = _client.DistTo(job);
            return (int)Math.Round(dist, 10);
        }
    }
}
