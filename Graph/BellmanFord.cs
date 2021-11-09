﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class BellmanFord : ShortestPath4WeightedDigraph
    {
        protected Queue<int> _freshV;

        protected int _cost;

        protected NegetiveCycleDetection _client;

        public bool HasCycle => _client.HasCycle;

        public IEnumerable<int> Cycle => _client.Cycle;

        public BellmanFord(EdgeWeightedDigraph g, int s) : base(g)
        {
            DistTo[s] = 0;
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

        protected virtual void Relax(int v)
        {
            foreach (Edge e in _g.Adj(v))
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
    }
}
