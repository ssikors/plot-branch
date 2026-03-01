using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Application.Services;

namespace PlotBranchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IGraphService _graphService;

        public GraphController(IGraphService graphService)
        {
            _graphService = graphService;
        }

        [HttpGet("health")]
        public ActionResult<string> GetHealth()
        {
            return Ok("Hello world");
        }

        /// <summary>
        /// Returns all PlotFlows - currently all in the database, in the future all for the current user
        /// </summary>
        /// <returns>PlotFlows</returns>
        [HttpGet]
        public async Task<IActionResult> GetFlows()
        {
            var flows = await _graphService.GetAllFlowsAsync();

            return Ok(flows);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlow([FromBody] CreatePlotFlowDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");

            CreatedPlotFlowDto createdDto = await _graphService.CreateFlowAsync(dto);

            return Ok(createdDto);
        }
    }
}
