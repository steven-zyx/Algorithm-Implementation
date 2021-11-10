using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPathInAGrid
    {
        protected SymbolWeightedDigraph<(int row, int col)> _symbolG;
        protected ShortestPath_DAG _client;
        protected int[,] _matrix;

        public ShortestPathInAGrid(int[,] matrix)
        {
            _matrix = matrix;
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            _symbolG = new SymbolWeightedDigraph<(int, int)>(row * column + 1);

            for (int r = 0; r < row; r++)
                for (int c = 0; c < column; c++)
                {
                    int nextR = r + 1;
                    if (nextR < row)
                        _symbolG.AddEdge((r, c), (nextR, c), matrix[nextR, c]);

                    int nextC = c + 1;
                    if (nextC < column)
                        _symbolG.AddEdge((r, c), (r, nextC), matrix[r, nextC]);
                }

            var s = (row, column);
            _symbolG.AddEdge(s, (0, 0), matrix[0, 0]);

            _client = new ShortestPath_DAG(_symbolG.G, _symbolG.Index[s]);
        }

        public IEnumerable<int> PathTo((int row, int col) destination)
        {
            int v = _symbolG.Index[destination];
            foreach (Edge e in _client.PathTo(v))
            {
                var pos = _symbolG.Names[e.To];
                yield return _matrix[pos.row, pos.col];
            }
        }
    }
}
