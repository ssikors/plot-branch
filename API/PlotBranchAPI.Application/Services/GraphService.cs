using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Managers;
using PlotBranchAPI.Business.Models;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Application.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphManager _graphManager;

        public GraphService(IGraphManager graphManager)
        {
            _graphManager = graphManager;
        }

        public async Task<CreatedPlotFlowDto> CreateFlowAsync(CreatePlotFlowDto flowDto)
        {
            var flow = new PlotFlow { Name = flowDto.Name };

            await _graphManager.CreateFlowAsync(flow);

            return new CreatedPlotFlowDto { Name = flow.Name, Id = flow.Id }; // TODO is this right
        }

        public async Task<List<PlotFlowListDto>> GetAllFlowsAsync()
        {
            List<PlotFlow> flows = await _graphManager.GetPlotFlowsAsync();
            return flows.Select(flow => new PlotFlowListDto { Name = flow.Name, Id = flow.Id }).ToList();
        }
    }
}
