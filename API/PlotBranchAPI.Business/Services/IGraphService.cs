using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Application.Services
{
    public interface IGraphService
    {
        public Task<List<PlotFlowListDto>> GetAllFlowsAsync();

        public Task<CreatedPlotFlowDto> CreateFlowAsync(CreatePlotFlowDto flowDto);
    }
}
