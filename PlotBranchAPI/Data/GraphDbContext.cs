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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NodeEntity>()
                .HasOne(n => n.Data)
                .WithOne(d => d.NodeEntity)
                .HasForeignKey<NodeData>(d => d.NodeEntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<PlotFlow> PlotFlows { get; set; }
        public DbSet<NodeEntity> Nodes { get; set; }
        public DbSet<EdgeEntity> Edges { get; set; }
        public DbSet<NodeData> NodesData { get; set; }
    }

}
