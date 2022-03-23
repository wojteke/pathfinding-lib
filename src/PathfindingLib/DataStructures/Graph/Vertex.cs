using PathfindingLib.API.DataStructures.Graph;

namespace PathfindingLib.DataStructures.Graph
{
	public class Vertex<TContent> : IVertex<TContent>
	{
		public TContent Content { get; set; }

		public Vertex()
		{

		}

		public Vertex(TContent content)
		{
			this.Content = content;
		}
	}
}
