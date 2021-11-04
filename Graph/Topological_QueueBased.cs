using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Topological_QueueBased
    {
        protected Queue<int> _order;

        public Topological_QueueBased(Digraph g)
        {
            DirectedCycle client = new DirectedCycle(g);
            IsDAG = !client.HasCycle;

            if (IsDAG)
            {
                int[] inDegrees = new int[g.V];
                for (int v = 0; v < g.V; v++)
                    foreach (int w in g.Adj(v))
                        inDegrees[w]++;

                Queue<int> sources = new Queue<int>();
                for (int v = 0; v < g.V; v++)
                    if (inDegrees[v] == 0)
                        sources.Enqueue(v);

                _order = new Queue<int>();
                while (sources.Count > 0)
                {
                    int source = sources.Dequeue();
                    _order.Enqueue(source);
                    foreach (int w in g.Adj(source))
                    {
                        inDegrees[w]--;
                        if (inDegrees[w] == 0)
                            sources.Enqueue(w);
                    }
                }
            }
        }

        public IEnumerable<int> Order() => _order;

        public bool IsDAG { get; }
    }
}
