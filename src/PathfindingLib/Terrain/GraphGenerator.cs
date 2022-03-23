using PathfindingLib.API.DataStructures.Graph;
using PathfindingLib.API.Terrain;
using PathfindingLib.Helpers;
using System;
using System.Collections.Generic;

namespace PathfindingLib.Terrain
{
	public class GraphGenerator : IGraphGenerator
	{
		public TGraph GenerateGraph<TGraph, TVertex, TContent, TEdge>(ITerrainData<TContent> terrainData, IVehicleData vehicleData, Func<ITerrainData<TContent>, IVehicleData, int, int, int, int, bool> validator)
			where TGraph : IGraph<TVertex, TContent, TEdge, double>, new()
			where TVertex : IVertex<TContent>, new()
			where TContent : IPoint3D, new()
			where TEdge : IEdgeLight<double>, new()
		{
			return this.GenerateGraph<TGraph, TVertex, TContent, TEdge>(terrainData, vehicleData, new List<Func<ITerrainData<TContent>, IVehicleData, int, int, int, int, bool>> { validator });
		}

		public TGraph GenerateGraph<TGraph, TVertex, TContent, TEdge>(ITerrainData<TContent> terrainData, IVehicleData vehicleData, IList<Func<ITerrainData<TContent>, IVehicleData, int, int, int, int, bool>> validators)
			where TGraph : IGraph<TVertex, TContent, TEdge, double>, new()
			where TVertex : IVertex<TContent>, new()
			where TContent : IPoint3D, new()
			where TEdge : IEdgeLight<double>, new()
		{
			var xLen = terrainData.Data.GetLength(0);
			var zLen = terrainData.Data.GetLength(1);

			var graph = new TGraph();

			int currentX;
			int currentZ;
			int index;
			bool valid;
			IPoint3D data;
			for (int x = 0; x < xLen; x++)
			{
				for (int z = 0; z < zLen; z++)
				{
					data = terrainData.Data[x, z];
					graph.AddVertex(new TContent() { X = data.X, Y = data.Y, Z = data.Z });
					index = graph.Count - 1;
					for (int xOffset = -1; xOffset <= 1; xOffset++)
					{
						for (int zOffset = -1; zOffset <= 1; zOffset++)
						{
							currentX = x + xOffset;
							currentZ = z + zOffset;
							if (!(currentX == x && currentZ == z) && currentX >= 0 && currentX < xLen && currentZ >= 0 && currentZ < zLen)
							{
								valid = true;
								for (int i = 0; i < validators.Count; i++)
								{
									if (!validators[i](terrainData, vehicleData, x, z, currentX, currentZ))
									{
										valid = false;
										break;
									}
								}
								if (valid)
								{
									graph.AddEdge(index, currentX * xLen + currentZ, terrainData.Data[x, z].DistanceTo(terrainData.Data[currentX, currentZ]));
								}
							}
						}
					}
				}
			}
			return graph;
		}
	}
}
