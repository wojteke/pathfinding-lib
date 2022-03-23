using System.Collections.Generic;

namespace PathfindingLib.API.DataStructures.Graph
{
	/// <summary>
	/// A basic interface for any graph.
	/// </summary>
	/// <typeparam name="TVertex">Vertex type</typeparam>
	/// <typeparam name="TContent">Vertex content</typeparam>
	/// <typeparam name="TEdge">Edge type</typeparam>
	/// <typeparam name="TWeight">Edge weight</typeparam>
	public interface IGraph<TVertex, TContent, TEdge, TWeight>
		where TVertex : IVertex<TContent>, new()
		where TEdge : IEdgeLight<TWeight>, new()
	{
		TVertex this[int key] { get; set; }

		int Count { get; }

		void AddVertex(TVertex content);

		void AddVertex(TContent content);

		void RemoveVertex(int index);

		void AddEdge(int startIndex, int endIndex, TWeight weight);

		void AddEdge(int startIndex, TEdge edge);

		IList<TEdge> GetEdges(int index);

		void AddEdgeBothWays(int startIndex, int endIndex, TWeight weight);

		void RemoveEdge(int startIndex, int endIndex);

		void RemoveEdgeBothWays(int startIndex, int endIndex);
	}
}
