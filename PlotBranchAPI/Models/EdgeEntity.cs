namespace PlotBranchAPI.Models
{
    public class EdgeEntity
    {
        public Guid Id { get; set; }

        public Guid Source { get; set; }
        public Guid Target { get; set; }
        public string? SourceHandle { get; set; }

        public Guid PlotFlowId { get; set; }
        public PlotFlow PlotFlow { get; set; }
    }
}
