namespace PlotBranchAPI.Models.Entities
{
    public class Character
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public Guid FlowId { get; set; }
        public required Story Flow { get; set; }
    }
}
