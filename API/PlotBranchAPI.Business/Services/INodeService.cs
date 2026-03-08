using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business.Services
{
    public interface INodeService
    {
        public Task<List<NodeDto>> GetAllNodes();

        public Task<List<NodeDto>> GetNodes(Guid plotFlowId);
                
        public Task<NodeDto> AddNode(CreateNodeDto createNodeDto);
        
        public Task<NodeDto> UpdateNode(NodeDto nodeToUpdateDto);
        
        public Task DeleteNode(Guid nodeId);
    }
}
