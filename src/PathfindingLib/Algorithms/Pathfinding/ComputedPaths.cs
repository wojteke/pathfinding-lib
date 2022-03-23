using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Algorithms.Pathfinding
{
	public class ComputedPaths
	{
		public int[] PreviousIndices { get; }

		public double[] Distances { get; }

		public int SourceIndex { get; }

		public ComputedPaths(int[] previousIndices, double[] distances, int sourceIndex)
		{
			this.PreviousIndices = previousIndices;
			this.Distances = distances;
			this.SourceIndex = sourceIndex;
		}
	}
}
