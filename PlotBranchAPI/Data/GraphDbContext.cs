namespace PlotBranchAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using PlotBranchAPI.Models;

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


        public DbSet<Character> Characters { get; set; }

        public DbSet<PlotFlow> PlotFlows { get; set; }
        public DbSet<NodeEntity> Nodes { get; set; }
        public DbSet<EdgeEntity> Edges { get; set; }
        public DbSet<NodeData> NodesData { get; set; }
    }

}
