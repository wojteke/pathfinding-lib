using System;

namespace PathfindingLib.DataStructures.Heap
{
	public class BinaryHeapWithKeyUpdate<TContent, TKey> : GenericBinaryHeapWithKeyUpdate<HeapNode<TContent, TKey>, TContent, TKey>
		where TKey : IComparable
	{
		public BinaryHeapWithKeyUpdate()
		{
		}

		public BinaryHeapWithKeyUpdate(int initialCapacity) : base(initialCapacity)
		{
		}
	}
}
