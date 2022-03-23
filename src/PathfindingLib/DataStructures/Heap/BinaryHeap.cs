using PathfindingLib.API.DataStructures.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.DataStructures.Heap
{
	public class BinaryHeap<TContent, TKey> : GenericBinaryHeap<HeapNode<TContent, TKey>, TContent, TKey>
		where TKey : IComparable
	{
		public BinaryHeap()
		{
		}

		public BinaryHeap(int initialCapacity) : base(initialCapacity)
		{
		}
	}
}
