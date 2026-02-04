namespace PlotBranchAPI.Models.GraphDto
{
    public class NodeDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Position Position { get; set; }
        public NodeData Data { get; set; }
        public Measured Measured { get; set; }

        public bool? Selected { get; set; }
        public bool? Dragging { get; set; }
    }
}
