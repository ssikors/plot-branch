using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public interface INodeRepository
    {
        public Task<NodeEntity?> GetNodeAsync(Guid nodeId);

        public Task<NodeEntity?> AddNodeAsync(NodeEntity node);

        public Task RemoveNodeAsync(NodeEntity node);

        public Task<NodeEntity> UpdateNodeAsync(NodeEntity node);

        public Task<List<NodeEntity>> GetAllNodesAsync();

        public Task<List<NodeEntity>> GetFlowNodesAsync(PlotFlow flow);
    }
}
