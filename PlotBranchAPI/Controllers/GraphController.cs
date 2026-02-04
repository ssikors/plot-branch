using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Models.GraphDto;

namespace PlotBranchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        [HttpGet("health")]
        public ActionResult<string> GetHealth()
        {
            return Ok("Hello world");
        }


        [HttpPost("save")]
        public IActionResult SaveGraph([FromBody] GraphDto flow)
        {
            Console.WriteLine("here");

            if (flow == null)
            {
                Console.WriteLine("here");
                return BadRequest("Flow is null");
            }
                

            // TODO  process flow
            Console.WriteLine($"Received {flow.Nodes?.Count} nodes");

            return Ok(new { message = "Flow received successfully" });
        }
    }
}
