namespace PlotBranchAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using PlotBranchAPI.Models.Entities;
    using PlotBranchAPI.Models.Graph;
    using PlotBranchAPI.Models.GraphDto;

    public class GraphDbContext : DbContext
    {
        public GraphDbContext(DbContextOptions<GraphDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
    }

}
