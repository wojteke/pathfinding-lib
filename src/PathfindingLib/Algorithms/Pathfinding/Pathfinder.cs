using PathfindingLib.API.DataStructures.Graph;
using PathfindingLib.API.DataStructures.PriorityQueue;
using PathfindingLib.DataStructures.PriorityQueue;
using System;
using System.Collections.Generic;

namespace PathfindingLib.Algorithms.Pathfinding
{
	public static class Pathfinder
	{
		public static ComputedPaths PriorityQueueDijkstra<TPriorityQueueWithKeyUpdate, TQueueItem, TVertex, TContent, TEdge>(IGraph<TVertex, TContent, TEdge, double> graph, int startIndex)
			where TPriorityQueueWithKeyUpdate : IPriorityQueueWithKeyUpdate<TQueueItem, int, double>, new()
			where TQueueItem : IQueueItem<int, double>, new()
			where TVertex : IVertex<TContent>, new()
			where TEdge : IEdgeLight<double>, new()
		{
			var heap = new TPriorityQueueWithKeyUpdate();
			heap.Clear(graph.Count);
			var previousIndices = new int[graph.Count];
			var distances = new double[graph.Count];

			previousIndices[startIndex] = startIndex;
			distances[startIndex] = 0;
			for (int i = 0; i < graph.Count; i++)
			{
				if (i != startIndex)
				{
					previousIndices[i] = -1;
					distances[i] = double.PositiveInfinity;
				}
				heap.Insert(i, distances[i]);
			}

			TQueueItem currentNode;
			for (int i = 0; i < graph.Count; i++)
			{
				currentNode = heap.ExtractMin();
				if (currentNode == null)
					break;
				var edges = graph.GetEdges(currentNode.Content);
				var countCache = edges.Count;
				for (int j = 0; j < countCache; j++)
				{
					var index = edges[j].EndIndex;
					var destinationPath = edges[j].Weight;

					if (distances[index] > distances[currentNode.Content] + destinationPath)
					{
						distances[index] = distances[currentNode.Content] + destinationPath;
						previousIndices[index] = currentNode.Content;
						heap.UpdateKey(index, distances[currentNode.Content] + destinationPath);
					}
				}
			}
			return new ComputedPaths(previousIndices, distances, startIndex);
		}

		public static ComputedPaths BinaryHeapDijkstra<TVertex, TContent, TEdge>(IGraph<TVertex, TContent, TEdge, double> graph, int startIndex)
			where TVertex : IVertex<TContent>, new()
			where TEdge : IEdgeLight<double>, new()
		{
			return PriorityQueueDijkstra<BinaryHeapPriorityQueueWithKeyUpdate<int, double>,
				BinaryHeapQueueItem<int, double>,
				TVertex,
				TContent,
				TEdge>(graph, startIndex);
		}

		public static PathResult AStar<TVertex, TContent, TEdge>(IGraph<TVertex, TContent, TEdge, double> graph, int startIndex, int endIndex,
			Func<TContent, TContent, double> heuristic)
			where TVertex : IVertex<TContent>, new()
			where TEdge : IEdgeLight<double>, new()
		{
			var closeSet = new HashSet<int>();
			var openSet = new BinaryHeapPriorityQueueWithKeyUpdate<int, double>();
			var previousIndices = new int[graph.Count];
			var distances = new double[graph.Count];
			openSet.Insert(startIndex, 0);
			for (int i = 0; i < graph.Count; i++)
			{
				if (i != startIndex)
				{
					previousIndices[i] = -1;
					distances[i] = double.PositiveInfinity;
				}
			}
			previousIndices[startIndex] = 0;
			distances[startIndex] = 0;
			while(openSet.Count > 0)
			{
				var currentNode = openSet.ExtractMin();
				if (currentNode.Content == endIndex)
				{
					break;
				}
				closeSet.Add(currentNode.Content);
				foreach (var edge in graph.GetEdges(currentNode.Content))
				{
					if (closeSet.Contains(edge.EndIndex))
						continue;
					var tentativeGScore = distances[currentNode.Content] + edge.Weight;
					if (!openSet.Contains(edge.EndIndex))
					{
						var hscore = heuristic(graph[edge.EndIndex].Content, graph[endIndex].Content);
						previousIndices[edge.EndIndex] = currentNode.Content;
						distances[edge.EndIndex] = tentativeGScore;
						openSet.Insert(edge.EndIndex, tentativeGScore + hscore);
					}
					else if(tentativeGScore < distances[edge.EndIndex])
					{
						var hscore = heuristic(graph[edge.EndIndex].Content, graph[endIndex].Content);
						previousIndices[edge.EndIndex] = currentNode.Content;
						distances[edge.EndIndex] = tentativeGScore;
						openSet.UpdateKey(edge.EndIndex, tentativeGScore + hscore);
					}
				}
			}
			var computedPaths = new ComputedPaths(previousIndices, distances, startIndex);
			return RebuildPath(computedPaths, endIndex);
		}

		private static (List<int>, double) ReconstructPath(int startIndex, Dictionary<int, (int, double)> cameFrom, int currentNode)
		{
			if (currentNode == startIndex)
				return (new List<int>() { startIndex }, 0);
			if (cameFrom.ContainsKey(currentNode))
			{
				var p = ReconstructPath(startIndex, cameFrom, cameFrom[currentNode].Item1);
				p.Item1.Add(currentNode);
				p.Item2 += cameFrom[currentNode].Item2;
				return p;
			}
			return (new List<int>(), 0);
		}

		public static PathResult RebuildPath(ComputedPaths computedPaths, int endIndex)
		{
			var output = new List<int>();
			var pathComplete = false;
			var currentIndex = endIndex;
			var totalLength = 0.0;
			for (int i = 0; i < computedPaths.PreviousIndices.Length; i++)
			{
				if (currentIndex == -1)
					break;
				output.Add(currentIndex);
				totalLength += computedPaths.Distances[currentIndex];
				// start index
				if (currentIndex == computedPaths.SourceIndex)
				{
					pathComplete = true;
					break;
				}
				currentIndex = computedPaths.PreviousIndices[currentIndex];
			}
			output.Reverse();
			return new PathResult(pathComplete ? output : new List<int>(), pathComplete ? totalLength : 0, pathComplete);
		}
	}
}
