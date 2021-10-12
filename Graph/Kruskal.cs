using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class Kruskal : IMST
    {
        protected EdgeWeightedGraph _g;
        protected Queue_N<Edge> _mstEdges;

        public double Weight { get; protected set; }

        public Kruskal(EdgeWeightedGraph g)
        {
            _g = g;
            _mstEdges = new Queue_N<Edge>();

            Edge[] edges = g.Edges().ToArray();
            QuickSort<Edge> client = new QuickSort<Edge>();
            client.Sort(edges);

            UnionFind uf = new UnionFind(g.V);
            foreach (Edge e in edges)
            {
                int v = e.Either(), w = e.Other(v);
                if (!uf.Connected(v, w))
                {
                    uf.Union(v, w);
                    _mstEdges.Enqueue(e);
                    Weight += e.Weight;
                    if (_mstEdges.Length == g.V - 1)
                        return;
                }
            }
        }

        public IEnumerable<Edge> Edges() => _mstEdges;
    }
}
