using PathfindingLib.API.DataStructures.PriorityQueue;
using System;

namespace PathfindingLib.API.DataStructures.Heap
{
	public interface IHeapNode<TContent, TKey>
		where TKey : IComparable
	{
		TContent Content { get; set; }

		TKey Key { get; set; }
	}
}
