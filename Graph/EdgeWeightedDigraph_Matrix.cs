using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedDigraph_Matrix : EdgeWeightedGraph_Matrix
    {
        public EdgeWeightedDigraph_Matrix(int v) : base(v) { }

        public EdgeWeightedDigraph_Matrix(int v, IEnumerable<(int, int, double)> edges) : base(v, edges) { }

        public override void AddEdge(int v, int w, double weight)
        {
            _matrix[v, w] = weight;
            E++;
        }

        public override IEnumerable<(int v, int w)> Edges()
        {
            for (int v = 0; v < V; v++)
                for (int w = 0; w < V; w++)
                    if (_matrix[v, w] != double.PositiveInfinity)
                        yield return (v, w);
        }
    }
}
