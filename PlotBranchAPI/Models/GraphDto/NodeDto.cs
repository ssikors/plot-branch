namespace PlotBranchAPI.Models.GraphDto
{
    public class NodeDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public NodePosition Position { get; set; }
        public NodeData Data { get; set; }
        public Measured Measured { get; set; }
    }
}
