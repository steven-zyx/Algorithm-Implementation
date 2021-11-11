using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class CriticalWeightedEdges : ShortestPath4WeightedDigraph
    {
        protected bool[] _duplicate;
        protected Queue<int> _freshV;
        protected int _t;

        public CriticalWeightedEdges(EdgeWeightedDigraph g, int s, int t) : base(g)
        {
            _t = t;
            DistTo[s] = 0;
            _duplicate = new bool[g.V];
            _freshV = new Queue<int>();

            _freshV.Enqueue(s);
            while (_freshV.Count > 0)
                Relax(_freshV.Dequeue());
        }

        protected virtual void Relax(int v)
        {
            foreach (Edge e in _g.Adj(v))
            {
                int w = e.To;
                double newDist = DistTo[v] + e.Weight;
                if (newDist < DistTo[w])
                {
                    DistTo[w] = newDist;
                    _edgeTo[w] = e;
                    _duplicate[w] = false;
                    _freshV.Enqueue(w);
                }
                else if (newDist == DistTo[w])
                    _duplicate[w] = true;
            }
        }

        public IEnumerable<Edge> CriticalEdges()
        {
            for (int v = _t; !_duplicate[v] && _edgeTo[v] != null; v = _edgeTo[v].From)
                yield return _edgeTo[v];
        }

        public bool[,] Sensitivity()
        {
            bool[,] matrix = new bool[_g.V, _g.V];
            foreach (Edge e in _g.Edges())
                matrix[e.From, e.To] = true;

            foreach (Edge e in CriticalEdges())
                matrix[e.From, e.To] = false;

            return matrix;
        }
    }
}