using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data.Entities;
using PlotBranchAPI.Data.Repositories;

namespace PlotBranchAPI.Application.Services
{
    public class EdgeService : IEdgeService
    {
        private readonly IEdgeRepository _edgeRepository;

        public EdgeService(IEdgeRepository edgeRepository)
        {
            _edgeRepository = edgeRepository;
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

            edge = await _edgeRepository.AddEdgeAsync(edge); // TODO error?

            return edge;
        }

        public async Task<List<EdgeDto>> GetEdgeDtosAsync(Guid plotFlowId)
        {
            List<EdgeEntity>? edges = await _edgeRepository.GetEdgesAsync(plotFlowId);

            if (edges == null)
            {
                throw new Exception();
            }

            return edges.Select(edge => new EdgeDto
            {
                Id = edge.Id,
                Source = edge.Source,
                SourceHandle = edge.SourceHandle,
                Target = edge.Target,
            }).ToList();
        }

        public async Task RemoveEdgeAsync(Guid edgeId)
        {
            EdgeEntity? edge = await _edgeRepository.GetEdgeAsync(edgeId);

            if (edge == null)
            {
                throw new EdgeNotFoundException();
            }

            await _edgeRepository.DeleteEdgeAsync(edge);
        }
    }
}
