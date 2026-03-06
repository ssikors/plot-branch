using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public interface ICharacterRepository
    {
        public Task AddCharacterAsync(Character character);
        public Task<NodeEntity?> GetNodeAsync(Guid nodeId);

        public Task<Character?> GetCharacterAsync(Guid id);

        public Task AddCharacterToNodeAsync(Character character, NodeEntity node);

        public Task<PlotFlow?> GetPlotWithCharactersAsync(Guid plotId);
    }
}
