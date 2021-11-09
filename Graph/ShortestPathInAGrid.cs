using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class ShortestPathInAGrid
    {
        public ShortestPathInAGrid(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(row * column + 1);

            for (int r = 0; r < row; r++)
                for (int c = 0; c < column; c++)
                {

                }
        }

        public IEnumerable<int> PathTo(int v) => throw new NotImplementedException();
    }
}
