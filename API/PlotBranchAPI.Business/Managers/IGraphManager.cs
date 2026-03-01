using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Business.Models;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business.Managers
{
    public interface IGraphManager
    {
        public Task<List<PlotFlow>> GetPlotFlowsAsync();
        public Task<PlotFlow> CreateFlowAsync(PlotFlow plotFlow);
    }
}
