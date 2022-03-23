namespace PathfindingLib.API.Terrain
{
	public interface ITerrainData<T> where T : IPoint3D
	{
		T[,] Data { get; set; }
	}
}
