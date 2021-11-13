using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class FastBellmanFord : ShortestPath4WeightedDigraph
    {
        protected Queue<int>[] _distance;
        protected int _maxWeight;
        protected int _shortestDistance;

        public FastBellmanFord(EdgeWeightedDigraph g, int s, int maxWeight) : base(g)
        {
            _maxWeight = maxWeight;
            DistTo[s] = 0;
            _distance = new Queue<int>[maxWeight * (g.V - 1) * 2 + 1 + 1];
            for (int i = 0; i < _distance.Length; i++)
                _distance[i] = new Queue<int>();
            _shortestDistance = 0 + maxWeight;
            _distance[_shortestDistance].Enqueue(s);
            _distance[_distance.Length - 1].Enqueue(-1);

            while (ResetShortestDistance())
                Relax();
        }

        protected void Relax()
        {
            int v = _distance[_shortestDistance].Dequeue();
            foreach (Edge e in _g.Adj(v))
            {
                int w = e.To;
                if (DistTo[w] > DistTo[v] + e.Weight)
                {
                    DistTo[w] = DistTo[v] + e.Weight;
                    _edgeTo[w] = e;

                    int distance = (int)DistTo[w] + _maxWeight;     //14.999999999999 -> 14 ?
                    if (!_distance[distance].Contains(w))
                        _distance[distance].Enqueue(w);
                    _shortestDistance = Math.Min(_shortestDistance, distance);
                }
            }
        }

        protected bool ResetShortestDistance()
        {
            while (_distance[_shortestDistance].Count() == 0)
                _shortestDistance++;
            return _shortestDistance != _distance.Length - 1;
        }
    }
}
