using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data.Entities;
using PlotBranchAPI.Data.Repositories;

namespace PlotBranchAPI.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IGraphRepository _graphRepository;


        public CharacterService(ICharacterRepository characterRepository, IGraphRepository graphRepository)
        {
            _characterRepository = characterRepository;
            _graphRepository = graphRepository;
        }

        public async Task<CharacterDto> AddCharacter(CreateCharacterDto characterDto, Guid plotId)
        {
            PlotFlow? plot = await _graphRepository.GetPlotFlowAsync(plotId);

            if (plot == null)
            {
                throw new PlotFlowNotFoundException();
            }

            Character character = new Character
            {
                Id = Guid.NewGuid(),
                Name = characterDto.Name,
                PlotFlowId = plotId,
                PlotFlow = plot
            };

            await _characterRepository.AddCharacterAsync(character);

            return new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
            };
        }

        public async Task AddCharacterToNode(CharacterToNodeDto characterToNodeDto, Guid nodeId)
        {
            var node = _characterRepository.GetNode(nodeId);

            if (node == null)
            {
                throw new NodeNotFoundException();
            }

            var character = _characterRepository.GetCharacter(characterToNodeDto.characterId);

            if (character == null)
            {
                throw new CharacterNotFoundException();
            }

            await _characterRepository.AddCharacterToNode(characterToNodeDto.characterId);
        }

        public async Task<List<CharacterDto>> GetPlotCharacters(Guid plotId)
        {
            PlotFlow? plot = await _characterRepository.GetPlotWithCharacters(plotId);

            if (plot == null)
            {
                throw new PlotFlowNotFoundException();
            }

            return plot.Characters.Select(character => new CharacterDto { Id = character.Id, Name = character.Name }).ToList();
        }
    }
}
