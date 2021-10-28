using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Application
{
    public class FordFolkerson
    {
        protected FlowEdge[] _edgeTo;
        protected FlowNetwork _g;
        protected int _s;
        protected int _t;
        public double Value { get; protected set; }

        public FordFolkerson((int from, int to, int cap)[] problem, int s, int t)
        {
            _g = new FlowNetwork(problem);
            _s = s;
            _t = t;

            while (HasAugmentingPath())
            {
                double bottle = double.PositiveInfinity;
                for (int v = _t; v != _s; v = _edgeTo[v].Other(v))
                    bottle = Math.Min(bottle, _edgeTo[v].ResidualCapacityTo(v));

                for (int v = _t; v != _s; v = _edgeTo[v].Other(v))
                    _edgeTo[v].AddFlowTo(v, bottle);
                Value += bottle;
            }
        }

        protected bool HasAugmentingPath()
        {
            _edgeTo = new FlowEdge[_g.V];
            bool[] marked = new bool[_g.V];
            Queue<int> vertices = new Queue<int>();

            marked[_s] = true;
            vertices.Enqueue(_s);

            while (vertices.Count > 0)
            {
                int v = vertices.Dequeue();
                foreach (FlowEdge e in _g.Adj(v))
                {
                    int w = e.Other(v);
                    if (!marked[w] && e.ResidualCapacityTo(w) > 0)
                    {
                        marked[w] = true;
                        _edgeTo[w] = e;
                        vertices.Enqueue(w);
                    }
                }
            }
            return marked[_t];
        }

        public StringBuilder ShowResult()
        {
            Func<double, double> fRound = x => Math.Round(x, 2);

            StringBuilder sb = new StringBuilder($"Max flow from {_s} to {_t} : {fRound(Value)}\r\n");
            for (int i = 0; i < _g.V; i++)
                foreach (FlowEdge e in _g.Adj(i))
                    if (e.From == i)
                        sb.Append($"{e.From} --> {e.To}\t{fRound(e.Capacity)}\t{fRound(e.Flow)}\r\n");
            return sb;
        }
    }
}
