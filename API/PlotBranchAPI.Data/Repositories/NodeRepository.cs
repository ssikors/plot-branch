using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly GraphDbContext _context;

        public NodeRepository(GraphDbContext context)
        {
            _context = context;
        }

        public async Task<NodeEntity?> GetNodeAsync(Guid nodeId)
        {
            return await _context.Nodes.FindAsync(nodeId);
        }
    }
}
