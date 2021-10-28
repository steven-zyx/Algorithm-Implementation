using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class Arbitrage
    {
        protected NegetiveCycleDetection _client;

        public Arbitrage(double[,] conversion)
        {
            int row = conversion.GetLength(0);
            int column = conversion.GetLength(1);
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(row);
            for (int r = 0; r < row; r++)
                for (int c = 0; c < column; c++)
                    if (r != c)
                        g.AddEdge(r, c, -Math.Log(conversion[r, c]));

            _client = new NegetiveCycleDetection(g);
        }

        public bool HasArbitrage => _client.HasCycle;

        public IEnumerable<int> Route => _client.Cycle;
    }
}
