using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Models;
using PlotBranchAPI.Data.Entities;
using PlotBranchAPI.Data.Repositories;

namespace PlotBranchAPI.Application.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;

        public GraphService(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }

        public async Task<CreatedPlotFlowDto> CreateFlowAsync(CreatePlotFlowDto flowDto)
        {
            var flow = new PlotFlow { Name = flowDto.Name };

            await _graphRepository.CreateFlowAsync(flow);

            return new CreatedPlotFlowDto { Name = flow.Name, Id = flow.Id }; // TODO is this right
        }

        public async Task<List<PlotFlowListDto>> GetAllFlowsAsync()
        {
            List<PlotFlow> flows = await _graphRepository.GetAllFlowsAsync();
            return flows.Select(flow => new PlotFlowListDto { Name = flow.Name, Id = flow.Id }).ToList();
        }
    }
}
