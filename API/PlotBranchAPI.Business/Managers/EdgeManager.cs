using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business.Managers
{
    public class EdgeManager : IEdgeManager
    {
        public Task<EdgeEntity> AddEdgeAsync(EdgeEntity edge)
        {

        }

        public Task<List<EdgeEntity>> GetEdgesAsync(Guid plotFlowId)
        {

        }

        public Task RemoveEdgeAsync(Guid edgeId)
        {

        }
    }
}
