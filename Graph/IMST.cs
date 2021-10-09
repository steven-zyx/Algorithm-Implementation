using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public interface IMST
    {
        double Weight { get; }

        IEnumerable<Edge> Edges();
    }
}
