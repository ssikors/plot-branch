namespace PlotBranchAPI.Data.Entities
{
    public class NodeData
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public Guid NodeEntityId { get; set; }
        public NodeEntity NodeEntity { get; set; }
    }
}
