using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class BitonicPath : EdgeWeightedPath
    {
        public bool IsIncreasing { get; set; }

        public HashSet<int> Vertices { get; set; }

        public override BitonicPath CopyAndAdd(Edge e)
        {
            BitonicPath newPath = new BitonicPath();
            newPath.Edges = new List<Edge>();
            newPath.Edges.AddRange(this.Edges);
            newPath.Edges.Add(e);
            newPath.Weight = this.Weight + e.Weight;
            newPath.Vertices = new HashSet<int>(this.Vertices);
            newPath.Vertices.Add(e.To);
            if (e.Weight < newPath.LastEdgeWeight)
                IsIncreasing = false;
            return newPath;
        }
    }
}
