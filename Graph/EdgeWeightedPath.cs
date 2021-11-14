using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmImplementation.Graph
{
    public class EdgeWeightedPath
    {
        public double Weight { get; set; }

        public List<Edge> Edges { get; set; }

        public int LastV => Edges[Edges.Count - 1].To;

        public double LastEdgeWeight => Edges[Edges.Count - 1].Weight;

        public virtual EdgeWeightedPath CopyAndAdd(Edge e)
        {
            EdgeWeightedPath newPath = new EdgeWeightedPath();
            newPath.Edges = new List<Edge>();
            newPath.Edges.AddRange(this.Edges);
            newPath.Edges.Add(e);
            newPath.Weight = this.Weight + e.Weight;
            return newPath;
        }
    }
}
