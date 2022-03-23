using PathfindingLib.API.DataStructures.Graph;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PathfindingLib.DataStructures.Graph
{
	/// <summary>
	/// Class providing generic types for vertices and edges.
	/// Graph is implemented by the successors lists inside vertices.
	/// </summary>
	/// <typeparam name="TVertex">Vertex implementation</typeparam>
	/// <typeparam name="TContent">Vertex content</typeparam>
	/// <typeparam name="TEdge">Edge implementation</typeparam>
	/// <typeparam name="TWeight">Edge weight</typeparam>
	public class AdjacencyListGenericGraph<TVertex, TContent, TEdge, TWeight> : IGraph<TVertex, TContent, TEdge, TWeight>
		where TVertex : IVertexWithEdges<TContent, TEdge, TWeight>, new()
		where TEdge : IEdgeLight<TWeight>, new()
	{
		protected int countCache;
		protected IList<TVertex> vertices = new List<TVertex>();
		public AdjacencyListGenericGraph()
		{

		}

		public AdjacencyListGenericGraph(int initialCapacity)
		{
			this.vertices = new List<TVertex>(initialCapacity);
		}

		public AdjacencyListGenericGraph(IList<TVertex> vertices)
		{
			this.vertices = vertices;
			this.UpdateCountCache();
		}

		public int Count => this.countCache;

		public TVertex this[int key]
		{
			get
			{
				return this.vertices[key];
			}
			set
			{
				this.vertices[key] = value;
			}
		}

		public void AddVertex(TContent content)
		{
			this.vertices.Add(new TVertex() { Content = content });
			this.UpdateCountCache();
		}

		public void AddVertex(TVertex vertex)
		{
			this.vertices.Add(vertex);
			this.UpdateCountCache();
		}

		public void AddEdge(int startIndex, TEdge edge)
		{
			this.vertices[startIndex].Edges.Add(edge);
		}

		public void AddEdge(int startIndex, int endIndex, TWeight weight)
		{
			this.vertices[startIndex].Edges.Add(new TEdge() { EndIndex = endIndex, Weight = weight });
		}

		public void AddEdgeBothWays(int startIndex, int endIndex, TWeight weight)
		{
			this.AddEdge(startIndex, endIndex, weight);
			this.AddEdge(endIndex, startIndex, weight);
		}

		public IList<TEdge> GetEdges(int index)
		{
			return this.vertices[index].Edges;
		}

		/// <summary>
		/// Requires full graph search - O(V+E).
		/// <br> May change order of the vertices. </br>
		/// </summary>
		/// <param name="index"></param>
		public void RemoveVertex(int index)
		{
			if (countCache == 0)
				return;

			// removes all edges connected to the deleted vertex
			for (int i = 0; i < this.countCache; i++)
			{
				this.RemoveEdgeBothWays(i, index);
			}

			// changes all the paths directed to the last element and redirects them to the deleted index
			TEdge edge;
			for (int i = 0; i < this.countCache; i++)
			{
				for (int j = 0; j < this.vertices[i].Edges.Count; j++)
				{
					edge = this.vertices[i].Edges[j];
					if (edge.EndIndex == countCache - 1)
					{
						edge.EndIndex = index;
					}
				}
			}

			// removes vertex by swap
			this.vertices[index] = this.vertices[countCache - 1];
			this.vertices.RemoveAt(countCache - 1);
			UpdateCountCache();
		}

		public void RemoveEdge(int startIndex, int endIndex)
		{
			this.vertices[startIndex].RemoveEdgeTo(endIndex);
		}

		public void RemoveEdgeBothWays(int startIndex, int endIndex)
		{
			this.vertices[startIndex].RemoveEdgeTo(endIndex);
			this.vertices[endIndex].RemoveEdgeTo(startIndex);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void UpdateCountCache()
		{
			this.countCache = this.vertices.Count;
		}
	}
}
