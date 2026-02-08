namespace PlotBranchAPI.Models
{
    public class NodeEntity
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        public string? DataJson { get; set; }

        public Guid PlotFlowId { get; set; }
        public PlotFlow PlotFlow { get; set; }
    }
}
