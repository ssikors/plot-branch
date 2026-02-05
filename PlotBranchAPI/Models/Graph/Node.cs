using PlotBranchAPI.Models.GraphDto;

namespace PlotBranchAPI.Models.Graph
{
    public class Node
    {
        public required string Id { get; set; }
        public required string Type { get; set; }
        public NodeData Data { get; set; }
        public List<Node>? ParentNodes { get; set; }
        public List<Node>? ChildNodes { get; set; }
    }
}
