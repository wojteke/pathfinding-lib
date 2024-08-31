# Pathfinding Lib
A simple pathfinding library

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Sample usage](#sample-usage)
  * [Basic graph operations](#basic-graph-operations)
  * [Built-in graph generation](#using-built-in-graph-generation-from-height-map)
  * [Pathfinding algorithms](#pathfinding-algorithms)

# General Info
Pathfinding Lib was created as my Bachelor's Degree project. It's goal was to create a fast, simple libraty to find optimal paths based on the terrain data and vehicle data.
## Currently algorithm data is based on:
* Terrain data (height map)
* Vehicle data (approach, exit angle and lateral swing angles)
## Using custom classes
Library was created so that everything is using public interfaces, so you can implement your own data structures to work with the library.

# Technologies
* C#
* .NET Standard 2.0

# Sample usage
CLI app is used to test the library. You can check the more examples there. (code may be a little messy though)

## Basic graph operations
```csharp
// Creating Graph
var graph = new AdjacencyListGraph<string, double>();

// Adding vertex (provide content of the vertex)
graph.AddVertex("vertex 1");
graph.AddVertex("vertex 2");
graph.AddVertex("vertex 3");

// Adding edge (based on indices of vertices)
graph.AddEdge(0, 1, 10);
graph.AddEdge(1, 2, 15);

// Removing edge
graph.RemoveEdge(1, 2);

// Removing vertex (edges are deleted automatically)
graph.RemoveVertex(0);
```

## Using built-in graph generation from height map
```csharp
// Creating terrain data
var terrainData = new TerrainData<Point3D>();
var size = 1000;
terrainData.Data = new Point3D[size, size];

// Populating with sample data
var rand = new Random(1234567);
for (int x = 0; x < size; x++)
{
	for (int z = 0; z < size; z++)
	{
		terrainData.Data[x, z] = new Point3D(x, rand.NextDouble() * 2, z);
	}
}

// Optional bilinear interpolation
// terrainData.Data = Reshaper.Terrain3DBilinearInterpolation<Point3D>(terrainData.Data, 100, 100);

// Vehicle Data
var vehicleData = new VehicleData();
vehicleData.ApproachAngle = Math.PI / 3;
vehicleData.ExitAngle = Math.PI / 3;
vehicleData.LeftSwingAngle = Math.PI / 4;
vehicleData.RightSwingAngle = Math.PI / 4;

// Creating generator
var generator = new GraphGenerator();

// Generating graph
// Last argument - Validator list is the edge validation - if you want full graph just pass EdgeValidators.AlwaysPass
var graph = generator.GenerateGraph<
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
	)
```


## Pathfinding algorithms
```csharp
// Djikstra
// Paths from source are stored in computed paths object
var computedPaths = Pathfinder.BinaryHeapDijkstra(graph, 0);
var path = Pathfinder.RebuildPath(computedPaths, graph.Count - 1);

// A*
// Returns a ready path
// Last argument is the heuristic method
var path = Pathfinder.AStar(graph, 0, graph.Count - 1, (p1, p2) => p1.DistanceTo(p2));
```
