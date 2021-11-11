using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace AlgorithmImplementation.Graph
{
    public class BiDirectionalSearch
    {
        public double[] DistTo { get; protected set; }
        protected Edge[] _edgeTo;
        protected IndexMinPQ<double> _indexQ;
        protected bool[] _outQ;
        protected EdgeWeightedDigraph _sequenceG;
        protected EdgeWeightedDigraph _reverseG;
        protected List<Edge> _spEdges;

        public BiDirectionalSearch(EdgeWeightedDigraph g, int s, int t)
        {
            int allV = g.V * 2;
            DistTo = new double[allV];
            for (int i = 0; i < allV; i++)
                DistTo[i] = double.PositiveInfinity;
            DistTo[s] = 0;
            DistTo[t + g.V] = 0;
            _edgeTo = new Edge[allV];
            _indexQ = new IndexMinPQ<double>(allV);
            _outQ = new bool[allV];
            _sequenceG = g;
            _reverseG = new EdgeWeightedDigraph(g.V * 2);
            foreach (Edge e in g.Edges())
                _reverseG.AddEdge(e.To + g.V, e.From + g.V, e.Weight);

            _indexQ.Insert(s, 0);
            _indexQ.Insert(t + g.V, 0);
            while (_indexQ.Size > 0)
            {
                int minV = _indexQ.DelMin();
                _outQ[minV] = true;

                int reverseV = minV < g.V ? minV + g.V : minV - g.V;
                if (!_outQ[reverseV])
                    Relax(minV);
            }

            int collideV = -1;
            Distance = double.PositiveInfinity;
            for (int v = 0; v < g.V; v++)
            {
                int reverseV = v + g.V;
                if (_outQ[v] && _outQ[reverseV] && DistTo[v] + DistTo[reverseV] < Distance)
                {
                    Distance = DistTo[v] + DistTo[reverseV];
                    collideV = v;
                }
            }

            _spEdges = new List<Edge>();
            Stack<Edge> forwardPart = new Stack<Edge>();
            for (int v = collideV; _edgeTo[v] != null; v = _edgeTo[v].From)
                forwardPart.Push(_edgeTo[v]);
            _spEdges.AddRange(forwardPart);
            for (int v = collideV + g.V; _edgeTo[v] != null; v = _edgeTo[v].From)
            {
                int from = _edgeTo[v].To - g.V;
                int to = _edgeTo[v].From - g.V;
                double weight = _edgeTo[v].Weight;
                _spEdges.Add(new Edge(from, to, weight));
            }
        }

        protected void Relax(int v)
        {
            IEnumerable<Edge> outEdges = null;
            if (v < _sequenceG.V)
                outEdges = _sequenceG.Adj(v);
            else
                outEdges = _reverseG.Adj(v);

            foreach (Edge e in outEdges)
            {
                int w = e.To;
                double newWeight = DistTo[v] + e.Weight;
                if (newWeight < DistTo[w])
                {
                    DistTo[w] = newWeight;
                    _edgeTo[w] = e;

                    if (_indexQ.Contains(w))
                        _indexQ.Change(w, DistTo[w]);
                    else
                        _indexQ.Insert(w, DistTo[w]);
                }
            }
        }

        public double Distance { get; }

        public IEnumerable<Edge> Edges => _spEdges;
    }
}