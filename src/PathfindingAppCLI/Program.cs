using PathfindingLib.Algorithms;
using PathfindingLib.Algorithms.Pathfinding;
using PathfindingLib.API.Terrain;
using PathfindingLib.DataStructures.Graph;
using PathfindingLib.DataStructures.PriorityQueue;
using PathfindingLib.Helpers;
using PathfindingLib.Terrain;
using PathfindingLib.Terrain.EdgeValidators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PathfindingAppCLI
{
	class Program
	{
		private static Random rand = new Random(1234567);
		static void Main(string[] args)
		{

			//var graph = new AdjacencyListGraph<int, double>(5);

			//graph.AddVertex(0);
			//graph.AddVertex(1);
			//graph.AddVertex(2);
			//graph.AddVertex(3);
			//graph.AddVertex(4);

			//graph.AddEdge(0, 1, 10);
			//graph.AddEdge(0, 2, 5);

			//graph.AddEdge(1, 2, 2);
			//graph.AddEdge(1, 3, 1);

			//graph.AddEdge(2, 1, 3);
			//graph.AddEdge(2, 3, 9);
			//graph.AddEdge(2, 4, 2);

			//graph.AddEdge(3, 4, 4);

			//graph.AddEdge(4, 3, 6);
			//graph.AddEdge(4, 0, 7);

			//graph.RemoveVertex(2);


			//for (int i = 0; i < graph.Count; i++)
			//{
			//	var node = graph[i];
			//	Console.WriteLine($"Node: {i}");
			//	Console.WriteLine($"Content: {node.Content}");
			//	var successorsCount = node.Edges.Count;
			//	for (int j = 0; j < successorsCount; j++)
			//	{
			//		Console.WriteLine($"Path to: {node.Edges[j].EndIndex}, distance: {node.Edges[j].Weight}");
			//	}
			//	Console.WriteLine();

			//}

			TestPathfinding(10);
			TestPathfinding(100);
			TestPathfinding(500);
			TestPathfinding(1000);

			Console.WriteLine("End");
			Console.ReadLine();
		}

		public static void GraphPerformanceText()
		{
			var size = 1000000;
			Console.WriteLine("Start");
			var graph = new AdjacencyListGraph<int, double>();

			for (int i = 0; i < size; i++)
			{
				graph.AddVertex(i);
			}

			Console.WriteLine("Added nodes");
			var count = graph.Count;

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					graph.AddEdge(i, rand.Next(count), rand.Next(1000));
				}
			}

			Console.WriteLine("Added edges");
		}

		public static void TestPathfinding(int size)
		{
			Console.WriteLine($"Size: {size}x{size}");
			Console.WriteLine($"Estimated vertices: {size*size}");

			var generator = new GraphGenerator();
			var terrainData = new TerrainData<Point3D>();
			terrainData.Data = new Point3D[size, size];
			for (int x = 0; x < size; x++)
			{
				for (int z = 0; z < size; z++)
				{
					terrainData.Data[x, z] = new Point3D(x, rand.NextDouble() * 2, z);
				}
			}
			// terrainData.Data = Reshaper.Terrain3DBilinearInterpolation<Point3D>(terrainData.Data, 100, 100);

			var vehicleData = new VehicleData();
			vehicleData.ApproachAngle = Math.PI / 3;
			vehicleData.ExitAngle = Math.PI / 3;
			vehicleData.LeftSwingAngle = Math.PI / 4;
			vehicleData.RightSwingAngle = Math.PI / 4;

			var newGraph = Stopwatcher.Track(() => generator.GenerateGraph<
				AdjacencyListGraph<Point3D, double>,
				VertexWithEdges<Point3D, EdgeLight<double>, double>,
				Point3D,
				EdgeLight<double>
				>(terrainData, vehicleData,
				new List<Func<ITerrainData<Point3D>, IVehicleData, int, int, int, int, bool>> {
					EdgeValidators.AlwaysPass,
					EdgeValidators.ApproachAndExitAngle,
					EdgeValidators.LeftAndRightSwingAngle
				}
			), "Graph generation");

			Stopwatcher.Track(() =>
			{
				var computedPaths = Pathfinder.BinaryHeapDijkstra(newGraph, 0);
				return Pathfinder.RebuildPath(computedPaths, newGraph.Count - 1);
			}, "Dijkstra");

			Stopwatcher.Track(() => Pathfinder.AStar(newGraph, 0, newGraph.Count - 1, (p1, p2) => p1.DistanceTo(p2)), "A*");
			Console.WriteLine();
		}
	}
}
