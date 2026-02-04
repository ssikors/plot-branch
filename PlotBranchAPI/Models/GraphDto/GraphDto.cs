using System.Xml.Linq;

namespace PlotBranchAPI.Models.GraphDto
{
    public class GraphDto
    {
        public List<NodeDto> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public Viewport Viewport { get; set; }
    }
}
