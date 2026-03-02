using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Managers;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Application.Services
{
    public class EdgeService : IEdgeService
    {
        private readonly IEdgeManager _edgeManager;

        public EdgeService(IEdgeManager edgeManager)
        {
            _edgeManager = edgeManager;
        }

        public async Task<EdgeEntity> AddEdgeAsync(CreateEdgeDto edgeDto)
        {
            EdgeEntity edge = new EdgeEntity
            {
                PlotFlowId = edgeDto.PlotFlowId,
                Source = edgeDto.Source,
                Target = edgeDto.Target,
                SourceHandle = edgeDto.SourceHandle
            };

            edge = await _edgeManager.AddEdgeAsync(edge);

            return edge;
        }

        public async Task<List<EdgeDto>> GetEdgeDtosAsync(Guid plotFlowId)
        {
            List<EdgeEntity> edges = await _edgeManager.GetEdgesAsync(plotFlowId);

            return edges.Select(edge => new EdgeDto
            {
                Id = edge.Id,
                Source = edge.Source,
                SourceHandle = edge.SourceHandle,
                Target = edge.Target,
            }).ToList();
        }

        public void RemoveEdgeAsync(Guid edgeId)
        {
            
        }
    }
}
}
