using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public interface IEdgeRepository
    {
        public Task<List<EdgeEntity>?> GetEdgesAsync(Guid plotId);

        public Task<EdgeEntity> AddEdgeAsync(EdgeEntity edge);

        public Task<EdgeEntity?> GetEdgeAsync(Guid edgeId);

        public Task DeleteEdgeAsync(EdgeEntity edge);
    }
}
