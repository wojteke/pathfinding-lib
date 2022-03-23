using System.Collections.Generic;

namespace PathfindingLib.API.DataStructures.Graph
{
	public interface IVertexWithEdges<TContent, TEdge, TWeight> : IVertex<TContent> where TEdge : IEdgeLight<TWeight>, new()
	{
		IList<TEdge> Edges { get; set; }

		void AddEdgeTo(int endIndex, TWeight weight);

		void RemoveEdgeTo(int endIndex);
	}
}
