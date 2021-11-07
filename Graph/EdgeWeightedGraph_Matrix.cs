using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedGraph_Matrix : IComparer<(int, int)>
    {
        protected double[,] _matrix;

        public int V { get; }

        public int E { get; protected set; }

        public EdgeWeightedGraph_Matrix(int v)
        {
            V = v;
            _matrix = new double[V, V];
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    _matrix[i, j] = double.PositiveInfinity;
        }

        public EdgeWeightedGraph_Matrix(int v, IEnumerable<(int, int, double)> edges) : this(v)
        {
            foreach (var edge in edges)
                AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        public virtual void AddEdge(int v, int w, double weight)
        {
            _matrix[v, w] = weight;
            _matrix[w, v] = weight;
            E++;
        }

        public IEnumerable<int> Adj(int v)
        {
            for (int i = 0; i < V; i++)
                if (_matrix[v, i] != double.PositiveInfinity)
                    yield return i;
        }

        public virtual IEnumerable<(int v, int w)> Edges()
        {
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    if (i <= j && _matrix[i, j] != double.PositiveInfinity)
                        yield return (i, j);
        }

        public double Weight(int v, int w) => _matrix[v, w];

        public int Compare((int, int) x, (int, int) y)
        {
            double wX = Weight(x.Item1, x.Item2);
            double wY = Weight(y.Item1, y.Item2);
            if (wX < wY)
                return -1;
            else if (wX > wY)
                return 1;
            else
                return 0;
        }
    }
}
