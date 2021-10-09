using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStrcture;

namespace AlgorithmImplementation.Graph
{
    public class Topological
    {
        protected Digraph _g;
        protected bool[] _marked;
        protected Stack_N<int> _order;
        protected DirectedCycle _client;

        public Topological(Digraph g)
        {
            _client = new DirectedCycle(g);
            _g = g;
            _marked = new bool[g.V];
            _order = new Stack_N<int>();
            for (int v = 0; v < _g.V; v++)
                if (!_marked[v])
                    DFS(v);
        }

        protected void DFS(int v)
        {
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!_marked[w])
                    DFS(w);
            _order.Push(v);
        }

        public IEnumerable<int> Order() => _order;

        public bool IsDAG() => !_client.HasCycle;
    }
}
