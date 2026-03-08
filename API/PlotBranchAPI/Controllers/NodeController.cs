using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Services;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeSevice;

        public NodeController(INodeService nodeService)
        {
            _nodeSevice = nodeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllNodes()
        {
            List<NodeDto> nodes = await _nodeSevice.GetAllNodes();

            return Ok(nodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetNodes([FromQuery] Guid plotFlowId)
        {
            try
            {
                var nodes = await _nodeSevice.GetNodes(plotFlowId);
                return Ok(nodes);
            } catch (PlotFlowNotFoundException)
            {
                return NotFound("PlotFlow not found");
            } catch ( Exception ex )
            {
                return BadRequest("Could not get nodes");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddNode(CreateNodeDto dto)
        {

            try
            {
                NodeDto nodeDto = await _nodeSevice.AddNode(dto);
                return Ok(nodeDto);
            }
            catch (PlotFlowNotFoundException)
            {
                return NotFound("PlotFlow not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Could not add node");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNode(Guid id, [FromBody] NodeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");


            try
            {
                NodeDto updatedDto = await _nodeSevice.UpdateNode(dto);
                return Ok(updatedDto);
            } catch (NodeNotFoundException)
            {
                return NotFound("Could not find the node");
            } catch (Exception e)
            {
                return BadRequest("Could not update node");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {

            try
            {
                await _nodeSevice.DeleteNode(id);
                return Ok();
            } catch (NodeNotFoundException)
            {
                return NotFound("Could not find the node to delete");
            } catch (Exception e)
            {
                return BadRequest("Could not delete node");
            }
        }
    }

}
