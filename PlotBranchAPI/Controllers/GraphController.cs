using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Business;
using PlotBranchAPI.Data;
using PlotBranchAPI.Models.DTOs;
using PlotBranchAPI.Models;
using PlotBranchAPI.Models.Entities;
using PlotBranchAPI.Models.GraphDto;

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

        // GET ALL FLOWS
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

        // CREATE NEW FLOW
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


        [HttpPost("save")]
        public IActionResult SaveGraph([FromBody] GraphDto flow)
        {

            if (flow == null)
            {
                return BadRequest("Flow is null");
            }
                

            Console.WriteLine($"Received {flow.Nodes?.Count} nodes");

            var nodes = GraphConverter.ConvertToNodeTree(flow);

            Console.WriteLine(nodes.Count);

            return Ok(new { message = "Flow received successfully" });
        }
    }
}
