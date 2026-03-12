using Moq;
using PlotBranchAPI.Application.DTOs;
using PlotBranchAPI.Business.Services;
using PlotBranchAPI.Data.Entities;
using PlotBranchAPI.Data.Repositories;

namespace PlotBranch.Tests

{
    public class NodeServiceTests
    {
        private readonly Mock<INodeRepository> _nodeRepositoryMock;
        private readonly Mock<IGraphRepository> _graphRepositoryMock;
        
        private readonly NodeService _nodeService;

        public NodeServiceTests()
        {
            _nodeRepositoryMock = new Mock<INodeRepository>();
            _graphRepositoryMock = new Mock<IGraphRepository>();

            _nodeService = new NodeService(_nodeRepositoryMock.Object, _graphRepositoryMock.Object);
        }

        [Fact]
        public async void AddNode_ShouldReturnDto_WhenSuccessful()
        {
            // Arrange
            Guid flowId = Guid.NewGuid();
            Guid nodeId = Guid.NewGuid();
            NodeDataDto nodeDataDto = new NodeDataDto { Description = "Description" };

            PlotFlow flow = new PlotFlow { Name = "Story", Id = flowId };

            CreateNodeDto createNodeDto = new CreateNodeDto { 
                PlotFlowId = flowId,
                Data = nodeDataDto,
                PositionX = 0,
                PositionY = 0,
                Type = "StoryNode"
            };

            NodeData nodeData = new NodeData { Description = "Description" };

            NodeEntity nodeEntity = new NodeEntity { Id = nodeId, PlotFlow = flow, Data = nodeData, PlotFlowId = flowId, PositionX = 0, PositionY = 0, Type ="StoryNode" };

            NodeDto expectedDto = new NodeDto { Id = nodeId, Data = nodeDataDto, PositionX=0, PositionY=0, Type = "StoryNode"};

            _graphRepositoryMock.Setup(repo => repo.GetPlotFlowAsync(flowId))
                .ReturnsAsync(flow);

            _nodeRepositoryMock.Setup(repo => repo.AddNodeAsync(It.Is<NodeEntity>(n =>
                n.Type == createNodeDto.Type &&
                n.PositionX == createNodeDto.PositionX &&
                n.PositionY == createNodeDto.PositionY
            ))).ReturnsAsync(nodeEntity);
            

            // Act
            NodeDto nodeDto = await _nodeService.AddNode(createNodeDto);

            // Assert
            Assert.Equivalent(expectedDto, nodeDto);
        }
    }
}