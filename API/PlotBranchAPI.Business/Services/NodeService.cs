using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Utils.Exceptions;
using PlotBranchAPI.Data.Entities;
using PlotBranchAPI.Data.Repositories;

namespace PlotBranchAPI.Business.Services
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IGraphRepository _graphRepository;

        public NodeService(INodeRepository nodeRepository, IGraphRepository graphRepository)
        {
            _nodeRepository = nodeRepository;
            _graphRepository = graphRepository;
        }

        public async Task<NodeDto> AddNode(CreateNodeDto createNodeDto)
        {
            PlotFlow? plotFlow = await _graphRepository.GetPlotFlowAsync(createNodeDto.PlotFlowId);

            if (plotFlow == null)
            {
                throw new PlotFlowNotFoundException();
            }

            NodeEntity node = new NodeEntity
            {
                Id = Guid.NewGuid(),
                Type = createNodeDto.Type,
                PlotFlow = plotFlow,
                PositionX = createNodeDto.PositionX,
                PositionY = createNodeDto.PositionY,
                Data = new NodeData
                {
                    Description = createNodeDto.Data?.Description
                }
            };

            node = await _nodeRepository.AddNodeAsync(node);

            return new NodeDto
            {
                Id = node!.Id,
                Type = node.Type,
                Data = new NodeDataDto { Description = node.Data.Description },
                PositionX = node.PositionX,
                PositionY = node.PositionY
            };
        }

        public async Task DeleteNode(Guid nodeId)
        {
            NodeEntity? node = await _nodeRepository.GetNodeAsync(nodeId);

            if (node == null)
            {
                throw new NodeNotFoundException();
            }

            await _nodeRepository.RemoveNodeAsync(node);
        }

        public async Task<List<NodeDto>> GetAllNodes()
        {
            List<NodeEntity> nodes = await _nodeRepository.GetAllNodesAsync();

            return nodes.Select(n => new NodeDto
            {
                Id = n.Id,
                Type = n.Type,
                Data = new NodeDataDto { Description = n.Data.Description, CharacterIds = n.Characters.Select(c => c.Id.ToString()).ToList() },
                PositionX = n.PositionX,
                PositionY = n.PositionY
            }).ToList();
        }

        public async Task<List<NodeDto>> GetNodes(Guid plotFlowId)
        {
            PlotFlow? flow = await _graphRepository.GetPlotFlowAsync(plotFlowId);

            if (flow == null)
            {
                throw new PlotFlowNotFoundException();
            }

            List<NodeEntity> nodes = await _nodeRepository.GetFlowNodesAsync(flow);

            return nodes.Select(n => new NodeDto
            {
                Id = n.Id,
                Type = n.Type,
                Data = new NodeDataDto { Description = n.Data.Description, CharacterIds = n.Characters.Select(c => c.Id.ToString()).ToList() },
                PositionX = n.PositionX,
                PositionY = n.PositionY
            }).ToList();
        }

        public async Task<NodeDto> UpdateNode(NodeDto nodeToUpdateDto)
        {
            NodeEntity? node = await _nodeRepository.GetNodeAsync(nodeToUpdateDto.Id);

            if (node == null)
            {
                throw new NodeNotFoundException();
            }

            node.Type = nodeToUpdateDto.Type;
            node.PositionX = nodeToUpdateDto.PositionX;
            node.PositionY = nodeToUpdateDto.PositionY;

            if (node.Data == null)
            {
                node.Data = new NodeData
                {
                    NodeEntityId = node.Id
                };
            }

            if (nodeToUpdateDto.Data != null)
            {
                node.Data.Description = nodeToUpdateDto.Data.Description;
            }


            node = await _nodeRepository.UpdateNodeAsync(node);

            return new NodeDto
            {
                Id = node.Id,
                Type = node.Type,
                Data = new NodeDataDto { Description = node.Data.Description, CharacterIds = node.Characters.Select(c => c.Id.ToString()).ToList() },
                PositionX = node.PositionX,
                PositionY = node.PositionY
            };
        }
    }
}
