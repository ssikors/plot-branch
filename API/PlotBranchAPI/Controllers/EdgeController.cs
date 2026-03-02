using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Data;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Application.Services;
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
            var edges = await _edgeService.GetEdgeDtosAsync(plotFlowId);

            return Ok(edges);
        }


        [HttpPost]
        public async Task<IActionResult> AddEdge(CreateEdgeDto dto)
        {

            EdgeEntity edge = await _edgeService.AddEdgeAsync(dto);

            return Ok(edge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdge(Guid id)
        {
            var edge = await _context.Edges.FindAsync(id);

            if (edge == null)
                return NotFound();

            _context.Edges.Remove(edge);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
