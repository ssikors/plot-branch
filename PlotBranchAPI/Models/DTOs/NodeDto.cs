namespace PlotBranchAPI.Models.DTOs
{
    public class CreateNodeDto
    {
        public Guid PlotFlowId { get; set; }
        public string Type { get; set; }
        public NodeDataDto? Data { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
    }


    public class NodeDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public NodeDataDto Data { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
    }


}
