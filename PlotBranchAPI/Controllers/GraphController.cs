using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Business;
using PlotBranchAPI.Data;
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
