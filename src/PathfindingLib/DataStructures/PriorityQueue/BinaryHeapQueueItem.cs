using PathfindingLib.API.DataStructures.PriorityQueue;
using PathfindingLib.DataStructures.Heap;
using System;

namespace PathfindingLib.DataStructures.PriorityQueue
{
	public class BinaryHeapQueueItem<TContent, TKey> : HeapNode<TContent, TKey>, IQueueItem<TContent, TKey>
		where TKey : IComparable
	{
	}
}
