using PlotBranchAPI.Business.Models;
using PlotBranchAPI.Business.Models.Converted;
using PlotBranchAPI.Data.Entities;

namespace PlotBranchAPI.Business
{
    public static class GraphConverter
    {
        public static List<TraversableNode> ConvertToNodeTree(PlotFlow flow)
        {
            if (flow == null || flow.Nodes == null)
                return new List<TraversableNode>();

            // Create lookup dictionary for fast node access
            var nodeLookup = flow.Nodes.ToDictionary(
                n => n.Id,
                n => new TraversableNode
                {
                    Id = n.Id,
                    Type = n.Type,
                    Data = n.Data,
                    ParentNodes = new List<TraversableNode>(),
                    ChildNodes = new List<TraversableNode>()
                });

            // Connect nodes using edges
            if (flow.Edges != null)
            {
                foreach (var edge in flow.Edges)
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
