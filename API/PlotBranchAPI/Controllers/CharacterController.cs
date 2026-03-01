using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data;


namespace PlotBranchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly GraphDbContext _context;

        public CharacterController(GraphDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters([FromQuery] Guid graphId)
        {
            var characters = await _context.Characters
                .Where(c => c.PlotFlowId == graphId)
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(characters);
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(
            [FromQuery] Guid graphId,
            [FromBody] CreateCharacterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Character name required");

            PlotFlow? plotFlow = await _context.PlotFlows
                .FindAsync(graphId);

            if (plotFlow == null)
                return NotFound("PlotFlow not found");

            var character = new Character
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                PlotFlowId = graphId,
                PlotFlow = plotFlow
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return Ok(new CharacterDto
            {
                Id = character.Id,
                Name = character.Name
            });
        }


        [HttpPost("Node")]
        public async Task<IActionResult> AddCharacterToNode(
                [FromQuery] Guid nodeId,
                [FromBody] CharacterToNodeDto dto
            )
        {
            NodeEntity? node = await _context.Nodes.FindAsync(nodeId);

            if (node == null)
            {
                return NotFound("Node not found");
            }

            Character? character = await _context.Characters.FindAsync(dto.characterId);

            if (character == null)
            {
                return NotFound("Character not found");
            }

            node.Characters.Add(character);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
