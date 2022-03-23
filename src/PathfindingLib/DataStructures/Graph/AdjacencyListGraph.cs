using System.Collections.Generic;

namespace PathfindingLib.DataStructures.Graph
{
	/// <summary>
	/// Graph created by using build in classes for vertex and edge. 
	/// <br> It overrides <see cref="AdjacencyListGenericGraph{TVertex,TContent,TEdge,TWeight}"/> class and injects build in types: </br>
	/// <br> - TVertex - <see cref="VertexWithEdges{TContent, TEdge, TWeight}"/> </br>
	/// <br> - TEdge - <see cref="EdgeLight{TWeight}"/> </br>
	/// </summary>
	/// <typeparam name="TContent">Content type of the vertices</typeparam>
	/// <typeparam name="TWeight">Weight type of the edges</typeparam>
	public class AdjacencyListGraph<TContent, TWeight> : AdjacencyListGenericGraph<VertexWithEdges<TContent, EdgeLight<TWeight>, TWeight>, TContent, EdgeLight<TWeight>, TWeight>
	{
		public AdjacencyListGraph()
		{

		}

		public AdjacencyListGraph(int initialCapacity) : base(initialCapacity)
		{

		}

		public AdjacencyListGraph(IList<VertexWithEdges<TContent, EdgeLight<TWeight>, TWeight>> vertices) : base(vertices)
		{

		}
	}
}
