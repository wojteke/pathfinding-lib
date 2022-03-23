using PathfindingLib.API.DataStructures.Graph;
using System.Collections.Generic;

namespace PathfindingLib.DataStructures.Graph
{
	public class VertexWithEdges<TContent, TEdge, TWeight>: Vertex<TContent>, IVertexWithEdges<TContent, TEdge, TWeight> where TEdge : IEdgeLight<TWeight>, new()
	{
		public VertexWithEdges()
		{

		}

		public VertexWithEdges(TContent content) : base(content)
		{

		}

		public IList<TEdge> Edges { get; set; } = new List<TEdge>();

		public void AddEdgeTo(int endIndex, TWeight weight)
		{
			this.Edges.Add(new TEdge() { EndIndex = endIndex, Weight = weight });
		}

		public void RemoveEdgeTo(int endIndex)
		{
			var count = Edges.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (Edges[i].EndIndex == endIndex)
					Edges.RemoveAt(i);
			}
		}
	}
}
