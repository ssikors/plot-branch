using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlotBranchAPI.Data;
using PlotBranchAPI.Models.DTOs;
using PlotBranchAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PlotBranchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdgeController : ControllerBase
    {
        private readonly GraphDbContext _context;

        public EdgeController(GraphDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetEdges([FromQuery] Guid plotFlowId)
        {
            var edges = await _context.Edges
                .Where(e => e.PlotFlowId == plotFlowId)
                .Select(e => new EdgeDto
                {
                    Id = e.Id,
                    Source = e.Source,
                    Target = e.Target,
                    SourceHandle = e.SourceHandle
                })
                .ToListAsync();

            return Ok(edges);
        }


        [HttpPost]
        public async Task<IActionResult> AddEdge(CreateEdgeDto dto)
        {
            var edge = new EdgeEntity
            {
                Id = Guid.NewGuid(),
                PlotFlowId = dto.PlotFlowId,
                Source = dto.Source,
                Target = dto.Target,
                SourceHandle = dto.SourceHandle
            };

            await _context.Edges.AddAsync(edge);
            await _context.SaveChangesAsync();

            return Ok(edge.Id);
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
