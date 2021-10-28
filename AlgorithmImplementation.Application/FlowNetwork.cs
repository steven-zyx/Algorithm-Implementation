using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Application
{
    public class FlowNetwork
    {
        public int V { get; }

        public int E { get; protected set; }

        protected Bag_L<FlowEdge>[] _adjacencyList;

        public FlowNetwork((int from, int to, int cap)[] problem) : this(problem.Length)
        {
            foreach (var pipe in problem)
                AddEdge(new FlowEdge(pipe.from, pipe.to, pipe.cap));
        }

        protected FlowNetwork(int v)
        {
            V = v;
            _adjacencyList = new Bag_L<FlowEdge>[V];
            for (int i = 0; i < V; i++)
                _adjacencyList[i] = new Bag_L<FlowEdge>();
        }

        public void AddEdge(FlowEdge e)
        {
            _adjacencyList[e.From].Add(e);
            _adjacencyList[e.To].Add(e);
            E++;
        }

        public IEnumerable<FlowEdge> Adj(int v) => _adjacencyList[v];

        public IEnumerable<FlowEdge> Edges()
        {
            for (int i = 0; i < V; i++)
                foreach (FlowEdge e in _adjacencyList[i])
                    if (e.From == i)
                        yield return e;
        }
    }
}
