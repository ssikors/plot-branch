using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business.Services
{
    public interface ICharacterService
    {
        public Task<List<CharacterDto>> GetPlotCharacters(Guid plotId);

        public Task<CharacterDto> AddCharacter(CreateCharacterDto characterDto, Guid plotId);

        public Task AddCharacterToNode(CharacterToNodeDto characterToNodeDto, Guid nodeId);
    }
}
