namespace PlotBranchAPI.Data.Entities
{
    public class PlotFlow
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public List<NodeEntity> Nodes { get; set; } = new();
        public List<EdgeEntity> Edges { get; set; } = new();
        public List<Character> Characters { get; set; } = new();
    }
}
