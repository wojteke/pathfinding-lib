using PathfindingLib.API.DataStructures.PriorityQueue;
using PathfindingLib.DataStructures.Heap;
using System;

namespace PathfindingLib.DataStructures.PriorityQueue
{
	public class BinaryHeapPriorityQueueWithKeyUpdate<TContent, TKey> : GenericBinaryHeapWithKeyUpdate<BinaryHeapQueueItem<TContent, TKey>, TContent, TKey>,
		IPriorityQueueWithKeyUpdate<BinaryHeapQueueItem<TContent, TKey>, TContent, TKey>
		where TKey : IComparable
	{
	}
}
