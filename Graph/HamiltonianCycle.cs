using System;
using System.Collections.Generic;
using System.Text;
using Searching;
using System.Linq;

namespace AlgorithmImplementation.Graph
{
    public class HamiltonianCycle
    {
        protected Graph _g;
        public bool IsHamiltonianCycle { get; protected set; }

        public HamiltonianCycle(Graph g)
        {
            _g = g;
            IsHamiltonianCycle = false;
        }

        public void Process()
        {
            Action<int[]> TryCase = path =>
            {
                int l = path.Length;
                if (!_g.Adj(path[0]).Contains(path[l - 1]))
                    return;
                for (int i = 1; i < l; i++)
                {
                    if (!_g.Adj(path[i]).Contains(path[i - 1]))
                        return;
                }
                IsHamiltonianCycle = true;
            };

            HeapsAlgorithm<int> client = new HeapsAlgorithm<int>(Enumerable.Range(0, _g.V).ToArray(), TryCase, int.MaxValue);
            client.Generate();
        }
    }
}
