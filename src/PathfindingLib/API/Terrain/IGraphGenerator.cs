using PathfindingLib.API.DataStructures.Graph;
using System;
using System.Collections.Generic;

namespace PathfindingLib.API.Terrain
{
	public interface IGraphGenerator
	{
		TGraph GenerateGraph<TGraph, TVertex, TContent, TEdge>(ITerrainData<TContent> terrainData, IVehicleData vehicleData, Func<ITerrainData<TContent>, IVehicleData, int, int, int, int, bool> validator)

		where TGraph : IGraph<TVertex, TContent, TEdge, double>, new()
		where TVertex : IVertex<TContent>, new()
		where TContent : IPoint3D, new()
		where TEdge : IEdgeLight<double>, new();

		TGraph GenerateGraph<TGraph, TVertex, TContent, TEdge>(ITerrainData<TContent> terrainData, IVehicleData vehicleData, IList<Func<ITerrainData<TContent>, IVehicleData, int, int, int, int, bool>> validators)
		where TGraph : IGraph<TVertex, TContent, TEdge, double>, new()
		where TVertex : IVertex<TContent>, new()
		where TContent : IPoint3D, new()
		where TEdge : IEdgeLight<double>, new();
	}
}
