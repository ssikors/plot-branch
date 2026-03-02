using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Application.Services
{
    public interface IEdgeService
    {
        public Task<List<EdgeDto>> GetEdgeDtosAsync(Guid plotFlowId);

        public Task<EdgeEntity> AddEdgeAsync(CreateEdgeDto edgeDto);

        public Task RemoveEdgeAsync(Guid edgeId);
    }
}
