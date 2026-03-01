using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Repositories;
using PlotBranchAPI.Business.Models;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business.Managers
{
    public class GraphManager : IGraphManager
    {
        private readonly IGraphRepository _repository;

        public GraphManager(IGraphRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlotFlow> CreateFlowAsync(PlotFlow plotFlow)
        {
            PlotFlow flow = await _repository.CreateFlowAsync(plotFlow);

            return flow;
        }

        public async Task<List<PlotFlow>> GetPlotFlowsAsync()
        {
            return await _repository.GetAllFlowsAsync();
        }
    }
}
