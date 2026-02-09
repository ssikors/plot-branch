namespace PlotBranchAPI.Models.Converted
{
    public class TraversableNode
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public NodeData? Data { get; set; }
        public List<TraversableNode> ParentNodes { get; set; } = [];
        public List<TraversableNode> ChildNodes { get; set; } = [];
    }
}
