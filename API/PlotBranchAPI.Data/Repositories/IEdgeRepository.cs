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
        public Task<EdgeEntity> GetEdgesAsync(Guid plotId);


    }
}
