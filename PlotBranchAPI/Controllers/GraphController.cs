using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
    }
}
