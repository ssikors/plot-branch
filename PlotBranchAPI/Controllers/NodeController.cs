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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllNodes()
        {
            var nodes = await _context.Nodes
                .Include(n => n.Data)
                .Select(n => new NodeDto
                {
                    Id = n.Id,
                    Type = n.Type,
                    Data = new NodeDataDto { Description = n.Data.Description },
                    PositionX = n.PositionX,
                    PositionY = n.PositionY
                })
                .ToListAsync();

            return Ok(nodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetNodes([FromQuery] Guid plotFlowId)
        {
            Console.WriteLine($"Query PlotFlowId: {plotFlowId}");

            var nodes = await _context.Nodes
                .Include(n => n.Data)
                .Include(n => n.Characters)
                .Where(n => n.PlotFlowId == plotFlowId)
                .Select(n => new NodeDto
                {
                    Id = n.Id,
                    Type = n.Type,
                    Data = new NodeDataDto { Description = n.Data.Description, CharacterIds = n.Characters.Select(c => c.Id.ToString()).ToList() },
                    PositionX = n.PositionX,
                    PositionY = n.PositionY
                })
                .ToListAsync();

            return Ok(nodes);
        }



        [HttpPost]
        public async Task<IActionResult> AddNode(CreateNodeDto dto)
        {
            var plotFlow = await _context.PlotFlows.FindAsync(dto.PlotFlowId);

            if (plotFlow == null)
            {
                return BadRequest("PlotFlow not found");
            }

            var node = new NodeEntity
            {
                Id = Guid.NewGuid(),
                Type = dto.Type,
                PlotFlow = plotFlow,
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNode(Guid id, [FromBody] NodeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            var node = await _context.Nodes
                .Include(n => n.Data)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (node == null)
                return NotFound();

            node.Type = dto.Type;
            node.PositionX = dto.PositionX;
            node.PositionY = dto.PositionY;

            if (node.Data == null)
            {
                node.Data = new NodeData
                {
                    NodeEntityId = node.Id
                };
            }

            if (dto.Data != null)
            {
                node.Data.Description = dto.Data.Description;
            }

            await _context.SaveChangesAsync();

            return Ok(dto);
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
