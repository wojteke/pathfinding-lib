using PathfindingLib.API.Terrain;

namespace PathfindingLib.Terrain
{
	public class TerrainData<T> : ITerrainData<T> where T : IPoint3D
	{
		public T[,] Data { get; set; }
	}
}
