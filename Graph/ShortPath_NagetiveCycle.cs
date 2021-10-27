using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPath_NagetiveCycle
    {
        protected EdgeWeightedDigraph _g;

        public double[] DistTo { get; protected set; }

        protected DirectedEdge[] _edgeTo;

        protected Queue<int> _freshV;

        protected int _cost;

        protected NegetiveCycleDetection _client;

        public bool HasCycle => _client.HasCycle;

        public IEnumerable<int> Cycle => _client.Cycle;

        public ShortestPath_NagetiveCycle(EdgeWeightedDigraph g, int s)
        {
            _g = g;
            DistTo = new double[_g.V];
            for (int i = 0; i < g.V; i++)
                DistTo[i] = double.PositiveInfinity;
            DistTo[s] = 0;
            _edgeTo = new DirectedEdge[g.V];

            _cost = 0;
            _client = new NegetiveCycleDetection(_g);

            _freshV = new Queue<int>();
            _freshV.Enqueue(s);
            while (_freshV.Count > 0 && !HasCycle)
                Relax(_freshV.Dequeue());
        }

        protected void DetectCycle()
        {
            EdgeWeightedDigraph spt = new EdgeWeightedDigraph(_g.V);
            for (int i = 0; i < _g.V; i++)
                if (_edgeTo[i] != null)
                    spt.AddEdge(_edgeTo[i]);
            _client = new NegetiveCycleDetection(spt);
        }

        protected void Relax(int v)
        {
            foreach (DirectedEdge e in _g.Adj(v))
            {
                int w = e.To;
                if (DistTo[w] > DistTo[v] + e.Weight)
                {
                    DistTo[w] = DistTo[v] + e.Weight;
                    _edgeTo[w] = e;
                    _freshV.Enqueue(w);
                }
                if (++_cost % _g.V == 0)
                    DetectCycle();
            }
        }

        public bool HasPathTo(int v) => DistTo[v] != double.PositiveInfinity;

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            Stack<DirectedEdge> edges = new Stack<DirectedEdge>();
            for (; _edgeTo[v] != null; v = _edgeTo[v].From)
                edges.Push(_edgeTo[v]);
            return edges;
        }
    }
}
