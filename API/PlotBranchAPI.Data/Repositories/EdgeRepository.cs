
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public class EdgeRepository : IEdgeRepository
    {
        private readonly GraphDbContext _context;

        public EdgeRepository(GraphDbContext context)
        {
            _context = context;
        }

        public async Task<EdgeEntity> AddEdgeAsync(EdgeEntity edge)
        {
            await _context.Edges.AddAsync(edge);
            await _context.SaveChangesAsync();
            return edge;
        }

        public async Task DeleteEdgeAsync(EdgeEntity edge)
        {
            _context.Edges.Remove(edge);
            await _context.SaveChangesAsync();
        }

        public async Task<EdgeEntity?> GetEdgeAsync(Guid edgeId)
        {
            return await _context.Edges.FindAsync(edgeId);
        }

        public async Task<List<EdgeEntity>?> GetEdgesAsync(Guid plotId)
        {
            PlotFlow? flow = await _context.PlotFlows.Include(p => p.Edges).Where(p => p.Id == plotId).FirstOrDefaultAsync();

            if (flow == null)
            {
                return null;
            }

            return flow.Edges;
        }
    }
}
