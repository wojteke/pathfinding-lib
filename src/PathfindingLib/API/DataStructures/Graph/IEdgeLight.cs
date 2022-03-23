namespace PathfindingLib.API.DataStructures.Graph
{
	public interface IEdgeLight<TWeight>
	{
		int EndIndex { get; set; }

		TWeight Weight { get; set; }
	}
}
