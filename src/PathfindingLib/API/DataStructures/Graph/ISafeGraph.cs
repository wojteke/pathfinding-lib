using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.API.DataStructures.Graph
{
	/// <summary>
	/// Interface providing declarations of methods for safe operations on graph
	/// </summary>
	/// <typeparam name="TVertex"></typeparam>
	/// <typeparam name="TContent"></typeparam>
	/// <typeparam name="TEdge"></typeparam>
	/// <typeparam name="TWeight"></typeparam>
	public interface ISafeGraph<TVertex, TContent, TEdge, TWeight> : IGraph<TVertex, TContent, TEdge, TWeight>
		where TVertex : IVertex<TContent>, new()
		where TEdge : IEdgeLight<TWeight>, new()
	{
		bool AddVertexSafe(TVertex vertex);

		void AddEdge(TVertex start, TVertex end, TWeight weight);

		void AddEdgeSafe(int startIndex, TEdge edge);

		void AddEdgeSafe(int startIndex, int endIndex, TWeight weight);
	}
}
