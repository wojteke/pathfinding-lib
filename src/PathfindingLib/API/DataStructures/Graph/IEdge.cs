namespace PathfindingLib.API.DataStructures.Graph
{
	public interface IEdge<TWeight>: IEdgeLight<TWeight>
	{
		int StartIndex { get; set; }
	}
}
