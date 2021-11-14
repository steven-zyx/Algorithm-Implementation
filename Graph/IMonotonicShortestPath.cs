using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public interface IMonotonicShortestPath
    {
        double DistTo(int v);

        IEnumerable<Edge> PathTo(int v);
    }
}
