using PathfindingLib.API.DataStructures.Graph;

namespace PathfindingLib.DataStructures.Graph
{
	public class EdgeLight<TWeight> : IEdgeLight<TWeight>
	{
		public EdgeLight()
		{

		}
		public EdgeLight(int endIndex, TWeight weight)
		{
			this.EndIndex = endIndex;
			this.Weight = weight;
		}

		public int EndIndex { get; set; }

		public TWeight Weight { get; set; }
	}
}
