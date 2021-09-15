using System;
using BasicDataStrcture;
using String;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Graph
    {
        public int V { get; }

        public int E { get; protected set; }

        protected Bag_L<int>[] _adjacencyList;

        public Graph(int[] data)
        {
            V = data[0];
            _adjacencyList = new Bag_L<int>[V];
            for (int i = 0; i < V; i++)
                _adjacencyList[i] = new Bag_L<int>();
            for (int i = 2; i < data.Length; i += 2)
                AddEdge(i, i + 1);
        }

        public void AddEdge(int v, int w)
        {
            _adjacencyList[v].Add(w);
            _adjacencyList[w].Add(v);
            E++;
        }

        public IEnumerable<int> Adj(int v) => _adjacencyList[v];

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{V} vertices, {E} edges");
            for (int i = 0; i < _adjacencyList.Length; i++)
                sb.AppendLine($"{i}: {string.Join(' ', _adjacencyList[i])}");
            return sb.ToString();
        }
    }
}
