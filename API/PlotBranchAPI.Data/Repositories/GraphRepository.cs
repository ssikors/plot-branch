using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public class GraphRepository : IGraphRepository
    {
        private readonly GraphDbContext _context;

        
        public GraphRepository(GraphDbContext context)
        {
            _context = context;
        }

        public async Task<PlotFlow> CreateFlowAsync(PlotFlow flow)
        {
            await _context.PlotFlows.AddAsync(flow);
            await _context.SaveChangesAsync();
            return flow;
        }

        public async Task<PlotFlow?> GetPlotFlowAsync(Guid flowId)
        {
            return await _context.PlotFlows.FindAsync(flowId);
        }

        public async Task<List<PlotFlow>> GetAllFlowsAsync()
        {
            return await _context.PlotFlows.ToListAsync();
        }
    }
}
