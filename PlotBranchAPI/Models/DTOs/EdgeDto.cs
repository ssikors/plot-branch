namespace PlotBranchAPI.Models.DTOs
{
    public class CreateEdgeDto
    {
        public Guid PlotFlowId { get; set; }
        public Guid Source { get; set; }
        public Guid Target { get; set; }
        public string? SourceHandle { get; set; }
    }

    public class EdgeDto
    {
        public Guid Id { get; set; }
        public Guid Source { get; set; }
        public Guid Target { get; set; }
        public string? SourceHandle { get; set; }
    }

}
