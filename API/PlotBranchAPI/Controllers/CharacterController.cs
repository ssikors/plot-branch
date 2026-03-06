using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Services;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data;


namespace PlotBranchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters([FromQuery] Guid graphId)
        {
            try
            {
                List<CharacterDto> characters = await _characterService.GetPlotCharacters(graphId);

                return Ok(characters);
            } catch (PlotFlowNotFoundException)
            {
                return NotFound("Could not find the plotflow");
            } catch (Exception ex)
            {
                return BadRequest("Could not get characters");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(
            [FromQuery] Guid graphId,
            [FromBody] CreateCharacterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Character name required");

            try
            {
                CharacterDto character = await _characterService.AddCharacter(dto, graphId);
                return Ok(character);
            } catch (PlotFlowNotFoundException)
            {
                return NotFound("The PlotFlow for this character was not found");
            } catch (Exception ex)
            {
                return BadRequest("Could not add character");
            }
        }


        [HttpPost("Node")]
        public async Task<IActionResult> AddCharacterToNode(
                [FromQuery] Guid nodeId,
                [FromBody] CharacterToNodeDto dto
            )
        {

            try
            {
                await _characterService.AddCharacterToNode(dto, nodeId);
                return Ok();
            }
            catch (NodeNotFoundException)
            {
                return NotFound("The Node for this character was not found");
            }
            catch (CharacterNotFoundException)
            {
                return NotFound("Could not find the character");
            }
            catch (Exception ex)
            {
                return BadRequest("Could not attach the character to node");
            }
        }
    }

}
