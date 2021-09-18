using System;
using System.Collections.Generic;
using System.Text;
using Searching;
using BasicDataStrcture;
using System.IO;

namespace AlgorithmImplementation.Graph
{
    public class SymbolGraph
    {
        public int V => _adjacencyList.Size();

        public int E { get; protected set; }

        protected ISymbolTable<int, Bag_L<int>> _adjacencyList;

        protected HashST<string, int> _index;

        protected HashST<int, string> _names;

        public SymbolGraph(string fileName, char del)
        {
            _adjacencyList = new SeperateChainingHashST<int, Bag_L<int>>();
            _index = new SeperateChainingHashST<string, int>();
            _names = new SeperateChainingHashST<int, string>();

            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] temp = line.Split(' ');
                foreach (string adja in temp[1].Split(del))
                    AddEdge(temp[0], adja);
            }
        }

        public void AddEdge(string v, string w)
        {
            if (!_index.Contains(v))
            {
                _index.Put(v, _index.Size());
                _names.Put(_names.Size(), v);
            }

            if (!_index.Contains(w))
            {
                _index.Put(w, _index.Size());
                _names.Put(_names.Size(), w);
            }

            AddEdge(_index.Get(v), _index.Get(w));
        }


        protected void AddEdge(int v, int w)
        {
            if (!_adjacencyList.Contains(v))
                _adjacencyList.Put(v, new Bag_L<int>());
            _adjacencyList.Get(v).Add(w);

            if (!_adjacencyList.Contains(w))
                _adjacencyList.Put(w, new Bag_L<int>());
            _adjacencyList.Get(w).Add(v);

            E++;
        }

        public IEnumerable<int> Adj(int v) => _adjacencyList.Get(v);

        public IEnumerable<string> Adj(string v)
        {
            int vertex = _index.Get(v);
            foreach (int adja in _adjacencyList.Get(vertex))
                yield return _names.Get(adja);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{V} vertices, {E} edges");
            foreach (string vertex in _index.Keys())
                sb.AppendLine($"{vertex}: {string.Join(' ', Adj(vertex))}");
            return sb.ToString();
        }
    }
}
