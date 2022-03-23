using PathfindingLib.API.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.DataStructures.Graph
{
	public class GenericSafeGraph<TVertex, TContent, TEdge, TWeight> : AdjacencyListGenericGraph<TVertex, TContent, TEdge, TWeight>, ISafeGraph<TVertex, TContent, TEdge, TWeight>
		where TVertex : IVertexWithEdges<TContent, TEdge, TWeight>, new()
		where TEdge : IEdgeLight<TWeight>, new()
	{
		/// <summary>
		/// Slower but checks for duplicates
		/// </summary>
		/// <param name="vertex"></param>
		public bool AddVertexSafe(TVertex vertex)
		{
			this.UpdateCountCache();
			for (int i = 0; i < this.countCache; i++)
			{
				if (this.vertices[i].Equals(vertex))
					return false;
			}
			this.vertices.Add(vertex);
			this.UpdateCountCache();
			return true;
		}

		/// <summary>
		/// Checks if vertices exist in graph
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="weight"></param>
		public void AddEdge(TVertex start, TVertex end, TWeight weight)
		{
			this.UpdateCountCache();
			var startIndex = -1;
			var endIndex = -1;
			for (int i = 0; i < this.countCache; i++)
			{
				if (this.vertices[i].Equals(start))
				{
					startIndex = i;
					if (endIndex != -1)
						break;
				}

				if (this.vertices[i].Equals(end))
				{
					endIndex = i;
					if (startIndex != -1)
						break;
				}
			}
			if (startIndex == -1 || endIndex == -1)
				return;

			this.AddEdge(startIndex, endIndex, weight);
		}

		/// <summary>
		/// Validates edge
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="weight"></param>
		public void AddEdgeSafe(int startIndex, TEdge edge)
		{
			if (startIndex == edge.EndIndex)
				return;

			this.UpdateCountCache();
			if (startIndex < 0 || startIndex >= this.countCache || edge.EndIndex < 0 || edge.EndIndex >= this.countCache)
				return;

			var successors = this.vertices[startIndex].Edges;
			var successorsCount = this.vertices[startIndex].Edges.Count;
			for (int i = 0; i < successorsCount; i++)
			{
				if (successors[i].EndIndex == edge.EndIndex)
					return;
			}

			this.vertices[startIndex].Edges.Add(edge);
		}

		public void AddEdgeSafe(int startIndex, int endIndex, TWeight weight)
		{
			if (startIndex == endIndex)
				return;

			this.UpdateCountCache();
			if (startIndex < 0 || startIndex >= this.countCache)
				return;

			if (endIndex < 0 || endIndex >= this.countCache)
				return;

			var successors = this.vertices[startIndex].Edges;
			var successorsCount = this.vertices[startIndex].Edges.Count;
			for (int i = 0; i < successorsCount; i++)
			{
				if (successors[i].EndIndex == endIndex)
					return;
			}

			this.AddEdge(startIndex, endIndex, weight);
		}
	}
}
