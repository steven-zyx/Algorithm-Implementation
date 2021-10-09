using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SCC
    {
        protected Digraph _g;
        protected bool[] _marked;

        public int Count { get; protected set; }
        public int[] ID { get; protected set; }

        public SCC(Digraph g)
        {
            _g = g;
            Count = 0;
            ID = new int[_g.V];
            _marked = new bool[_g.V];
            Topological client = new Topological(_g.Reverse());
            foreach (int v in client.Order())
                if (!_marked[v])
                {
                    DFS(v);
                    Count++;
                }
        }

        protected void DFS(int v)
        {
            ID[v] = Count;
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!_marked[w])
                    DFS(w);
        }

        public bool StronglyConnected(int v, int w) => ID[v] == ID[w];

    }
}
