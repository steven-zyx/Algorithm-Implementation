using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmImplementation.Graph
{
    public class ConnectedComponent : UndirectedGraphSearch
    {
        public int ComponentCount { get; protected set; }
        public int[] ID { get; protected set; }

        public ConnectedComponent(Graph g) : base(g, 0)
        {
            ComponentCount = 0;
            ID = new int[_g.V];
        }

        public override void Process()
        {
            for (int i = 0; i < _g.V; i++)
                if (!Marked[i])
                {
                    Search(i);
                    ComponentCount++;
                }
        }

        protected override void Search(int v)
        {
            ID[v] = ComponentCount;
            Marked[v] = true;
            foreach (int w in _g.Adj(v))
                if (!Marked[w])
                    Search(w);
        }
    }
}
