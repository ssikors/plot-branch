namespace PlotBranchAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using PlotBranchAPI.Models;
    using PlotBranchAPI.Models.Entities;
    using PlotBranchAPI.Models.Graph;
    using PlotBranchAPI.Models.GraphDto;

    public class GraphDbContext : DbContext
    {
        public GraphDbContext(DbContextOptions<GraphDbContext> options)
            : base(options)
        {
        }

        public DbSet<PlotFlow> PlotFlows { get; set; }
        public DbSet<NodeEntity> Nodes { get; set; }
        public DbSet<EdgeEntity> Edges { get; set; }
    }

}
