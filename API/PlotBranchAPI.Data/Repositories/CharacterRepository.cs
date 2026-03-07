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
        private readonly GraphDbContext _context;

        public CharacterRepository(GraphDbContext graphDbContext)
        {
            _context = graphDbContext;
        }

        public async Task AddCharacterAsync(Character character)
        {
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();
        }

        public async Task AddCharacterToNodeAsync(Character character, NodeEntity node)
        {
            node.Characters.Add(character);
            await _context.SaveChangesAsync();
        }

        public async Task<Character?> GetCharacterAsync(Guid id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<List<Character>> GetPlotCharactersAsync(PlotFlow plot)
        {
            return await _context.Characters.Where(character => character.PlotFlow == plot).ToListAsync();
        }
    }
}
