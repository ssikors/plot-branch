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
    public class NodeController : ControllerBase
    {
        private readonly GraphDbContext _context;

        public NodeController(GraphDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNodes([FromQuery] Guid plotFlowId)
        {
            var nodes = await _context.Nodes
                .Include(n => n.Data)
                .Where(n => n.PlotFlowId == plotFlowId)
                .Select(n => new NodeDto
                {
                    Id = n.Id,
                    Type = n.Type,
                    Data = new NodeDataDto { Description = n.Data.Description},
                    PositionX = n.PositionX,
                    PositionY = n.PositionY
                })
                .ToListAsync();

            return Ok(nodes);
        }



        [HttpPost]
        public async Task<IActionResult> AddNode(CreateNodeDto dto)
        {
            var node = new NodeEntity
            {
                Id = Guid.NewGuid(),
                Type = dto.Type,
                PlotFlowId = dto.PlotFlowId,
                PositionX = dto.PositionX,
                PositionY = dto.PositionY,
                Data = new NodeData
                {
                    Description = dto.Data.Description
                }
            };

            await _context.Nodes.AddAsync(node);
            await _context.SaveChangesAsync();


            var newNode = new NodeDto
            {
                Id = node.Id,
                Type = node.Type,
                Data = new NodeDataDto { Description = node.Data.Description },
                PositionX = node.PositionX,
                PositionY = node.PositionY
            };

            return Ok(newNode);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            var node = await _context.Nodes
                .Include(n => n.Data)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (node == null)
                return NotFound();

            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
