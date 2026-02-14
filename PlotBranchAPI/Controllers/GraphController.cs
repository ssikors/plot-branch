using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Data;
using PlotBranchAPI.Models;
using PlotBranchAPI.Models.DTOs;

namespace PlotBranchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly GraphDbContext _context;

        public GraphController(GraphDbContext context)
        {
            _context = context;
        }

        [HttpGet("health")]
        public ActionResult<string> GetHealth()
        {
            return Ok("Hello world");
        }

        [HttpGet]
        public async Task<IActionResult> GetFlows()
        {
            var flows = _context.PlotFlows
                .Select(f => new PlotFlowListDto
                {
                    Id = f.Id.ToString(),
                    Name = f.Name
                })
                .ToList();

            return Ok(flows);
        }

        /// <summary>
        /// Initializes a new, empty flow
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Flow id and name</returns>
        [HttpPost]
        public async Task<IActionResult> CreateFlow([FromBody] CreatePlotFlowDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");

            var flow = new PlotFlow
            {
                Name = dto.Name
            };

            await _context.PlotFlows.AddAsync(flow);
            await _context.SaveChangesAsync();

            return Ok(new { flow.Id, flow.Name });
        }
    }
}
