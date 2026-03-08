using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<NodeEntity?> AddNodeAsync(NodeEntity node)
        {
            await _context.Nodes.AddAsync(node);
            await _context.SaveChangesAsync();

            return node;
        }

        public async Task<List<NodeEntity>> GetAllNodesAsync()
        {
            return await _context.Nodes.ToListAsync();
        }

        public async Task<List<NodeEntity>> GetFlowNodesAsync(PlotFlow flow)
        {
            return await _context.Nodes.Where(n => n.PlotFlow == flow).ToListAsync();
        }

        public async Task<NodeEntity?> GetNodeAsync(Guid nodeId)
        {
            return await _context.Nodes.FindAsync(nodeId);
        }

        public async Task RemoveNodeAsync(NodeEntity node)
        {
            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();
        }

        public async Task<NodeEntity> UpdateNodeAsync(NodeEntity node)
        {
            _context.Nodes.Update(node);
            await _context.SaveChangesAsync();
            return node;
        }
    }
}
