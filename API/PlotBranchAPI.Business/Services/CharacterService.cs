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
        private readonly INodeRepository _nodeRepository;


        public CharacterService(
            ICharacterRepository characterRepository,
            IGraphRepository graphRepository,
            INodeRepository nodeRepository)
        {
            _characterRepository = characterRepository;
            _graphRepository = graphRepository;
            _nodeRepository = nodeRepository;
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
            var node = await _nodeRepository.GetNodeAsync(nodeId);

            if (node == null)
            {
                throw new NodeNotFoundException();
            }

            var character = await _characterRepository.GetCharacterAsync(characterToNodeDto.characterId);

            if (character == null)
            {
                throw new CharacterNotFoundException();
            }

            await _characterRepository.AddCharacterToNodeAsync(character, node);
        }

        public async Task<List<CharacterDto>> GetPlotCharacters(Guid plotId)
        {
            PlotFlow? plot = await _graphRepository.GetPlotFlowAsync(plotId);

            if (plot == null)
            {
                throw new PlotFlowNotFoundException();
            }

            List<Character> characters = await _characterRepository.GetPlotCharactersAsync(plot);

            return plot.Characters.Select(character => new CharacterDto { Id = character.Id, Name = character.Name }).ToList();
        }
    }
}
