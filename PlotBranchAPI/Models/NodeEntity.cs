namespace PlotBranchAPI.Models
{
    public class NodeEntity
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        
        public NodeData Data { get; set; }

        public Guid PlotFlowId { get; set; }
        public required PlotFlow PlotFlow { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public List<Character> Characters { get; set; } = [];
    }
}
