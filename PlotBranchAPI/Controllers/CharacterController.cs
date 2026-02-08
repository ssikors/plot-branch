using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data;
using PlotBranchAPI.Models.Entities;

namespace PlotBranchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {

        private readonly GraphDbContext _context;

        public CharacterController(GraphDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(CharacterDto character, Guid flowId)
        {

            if (character == null)
            {
                return BadRequest("Character is null");
            }

            var flow = await _context.FindAsync<Story>(flowId);

            if (flow == null)
            {
                return BadRequest("Flow is null");
            }

            var newCharacter = new Character { Name = character.Name, Flow = flow };

            await _context.AddAsync(newCharacter);

            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCharacters(Guid flowId)
        //{
        //    var characters =  await _context.Characters.Where(c => c.FlowId == flowId).ToListAsync();

        //    return Ok(characters);
        //}
    }
}
