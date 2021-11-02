using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class SCC_Extended : SCC
    {
        public HashSet<int>[] IDVertices { get; }

        public Digraph KernalDAG { get; }

        public SCC_Extended(Digraph g) : base(g)
        {
            IDVertices = new HashSet<int>[Count];
            for (int i = 0; i < Count; i++)
                IDVertices[i] = new HashSet<int>();
            for (int v = 0; v < _g.V; v++)
                IDVertices[ID[v]].Add(v);

            HashSet<int>[] adjacencySet = new HashSet<int>[Count];
            for (int i = 0; i < Count; i++)
                adjacencySet[i] = new HashSet<int>();
            for (int v = 0; v < _g.V; v++)
                foreach (int w in _g.Adj(v))
                    if (!StronglyConnected(v, w))
                        adjacencySet[ID[v]].Add(ID[w]);

            KernalDAG = new Digraph(Count);
            for (int i = 0; i < Count; i++)
                foreach (int w in adjacencySet[i])
                    KernalDAG.AddEdge(i, w);
        }
    }
}
