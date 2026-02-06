using PlotBranchAPI.Models.Graph;
using PlotBranchAPI.Models.GraphDto;

namespace PlotBranchAPI.Business
{
    public static class GraphConverter
    {
        public static List<Node> ConvertToNodeTree(GraphDto graph)
        {
            if (graph == null || graph.Nodes == null)
                return new List<Node>();

            // Create lookup dictionary for fast node access
            var nodeLookup = graph.Nodes.ToDictionary(
                n => n.Id,
                n => new Node
                {
                    Id = n.Id,
                    Type = n.Type,
                    Data = n.Data,
                    ParentNodes = new List<Node>(),
                    ChildNodes = new List<Node>()
                });

            // Connect nodes using edges
            if (graph.Edges != null)
            {
                foreach (var edge in graph.Edges)
                {
                    if (!nodeLookup.TryGetValue(edge.Source, out var parent))
                        continue;

                    if (!nodeLookup.TryGetValue(edge.Target, out var child))
                        continue;

                    parent.ChildNodes!.Add(child);
                    child.ParentNodes!.Add(parent);
                }
            }

            return nodeLookup.Values.ToList();
        }
    }

}
