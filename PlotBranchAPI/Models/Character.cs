namespace PlotBranchAPI.Models
{
    public class Character
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid PlotFlowId { get; set; }
        public required PlotFlow PlotFlow { get; set; }
        public List<NodeEntity> Nodes { get; set; } = [];
    }
}
