using PathfindingLib.API.DataStructures.PriorityQueue;
using PathfindingLib.DataStructures.Heap;
using System;

namespace PathfindingLib.DataStructures.PriorityQueue
{
	public class BinaryHeapPriorityQueue<TContent, TKey> : GenericBinaryHeap<BinaryHeapQueueItem<TContent, TKey>, TContent, TKey>, 
		IPriorityQueue<BinaryHeapQueueItem<TContent, TKey>, TContent, TKey>
		where TKey : IComparable
	{

	}
}
