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

        public StringBuilder ShowResult(double[,] conversion, Dictionary<int, string> indexName)
        {
            StringBuilder sb = new StringBuilder("Conversion rate table:\r\n");
            int row = conversion.GetLength(0);
            int column = conversion.GetLength(1);
            for (int r = 0; r < row; r++)
            {
                sb.Append(indexName[r]);
                sb.Append(":\t");
                for (int c = 0; c < column; c++)
                {
                    sb.Append(conversion[r, c]);
                    sb.Append("\t");
                }
                sb.Append("\r\n");
            }

            sb.Append("\r\nArbitrage:\r\n");
            foreach (int index in Route)
            {
                sb.Append(" --> ");
                sb.Append(indexName[index]);
            }

            sb.Append("Value:\r\n1");
            int[] route = Route.ToArray();
            double arbiRate = 1;
            for (int i = 1; i < route.Length; i++)
            {
                double rate = conversion[route[i - 1], route[i]];
                arbiRate *= rate;
                sb.Append(" * ");
                sb.Append(Math.Round(rate, 3));
            }
            sb.Append(" = ");
            sb.Append(Math.Round(arbiRate, 5));

            sb.Append("Path length:\r\n(0");
            double nLength = 0;
            for (int i = 1; i < route.Length; i++)
            {
                double rate = conversion[route[i - 1], route[i]];
                double pathLength = Math.Log(rate);
                nLength += pathLength;
                sb.Append(") + (");
                sb.Append(Math.Round(pathLength, 3));
            }
            sb.Append(") = (");
            sb.Append(Math.Round(nLength, 5));
            sb.Append(")");

            return sb;
        }
    }
}
