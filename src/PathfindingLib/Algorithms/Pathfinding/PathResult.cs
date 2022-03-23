using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathfindingLib.Algorithms.Pathfinding
{
	public class PathResult
	{
		public List<int> Indices { get; }

		public double TotalLength { get; }

		public bool PathExists { get; }

		public PathResult(IEnumerable<int> indices, double totalLength, bool pathExists)
		{
			this.Indices = indices.ToList();
			this.TotalLength = totalLength;
			this.PathExists = pathExists;
		}
	}
}
