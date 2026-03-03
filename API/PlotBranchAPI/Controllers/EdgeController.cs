using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Application.Services;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data.Entities;


namespace PlotBranchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdgeController : ControllerBase
    {
        private readonly IEdgeService _edgeService;

        public EdgeController(IEdgeService edgeService)
        {
            _edgeService = edgeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetEdges([FromQuery] Guid plotFlowId)
        {
            try
            {
                var edges = await _edgeService.GetEdgeDtosAsync(plotFlowId);

                return Ok(edges);
            }  catch (Exception)
            {
                return BadRequest("Could not retrieve edges");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddEdge(CreateEdgeDto dto)
        {
            try
            {
                EdgeEntity edge = await _edgeService.AddEdgeAsync(dto);

                return Ok(edge);
            } catch (Exception)
            {
                return BadRequest("Could not add edge");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdge(Guid id)
        {
            try
            {
                await _edgeService.RemoveEdgeAsync(id);
                return Ok();
            } catch (EdgeNotFoundException)
            {
                return NotFound("Edge not found");
            } catch (Exception)
            {
                return BadRequest("Could not delete edge");
            }
        }
    }

}
