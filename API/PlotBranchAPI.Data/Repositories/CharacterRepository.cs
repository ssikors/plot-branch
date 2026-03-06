using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly GraphDbContext _graphDbContext;

        public CharacterRepository(GraphDbContext graphDbContext)
        {
            _graphDbContext = graphDbContext;
        }

        public async Task AddCharacterAsync(Character character)
        {
            await _graphDbContext.AddAsync(character);
            await _graphDbContext.SaveChangesAsync();
        }

        public async Task AddCharacterToNodeAsync(Character character, NodeEntity node)
        {
            node.Characters.Add(character);
            await _graphDbContext.SaveChangesAsync();
        }

        public async Task<Character?> GetCharacterAsync(Guid id)
        {
            return await _graphDbContext.Characters.FindAsync(id);
        }

        public async Task<NodeEntity?> GetNodeAsync(Guid nodeId)
        {
            return await _graphDbContext.Nodes.FindAsync(nodeId);
        }

        public async Task<PlotFlow?> GetPlotWithCharactersAsync(Guid plotId)
        {
            return await _graphDbContext.PlotFlows.Include(f => f.Characters).Where(f => f.Id == plotId).FirstOrDefaultAsync();
        }
    }
}
