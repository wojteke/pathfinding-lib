using PathfindingLib.API.DataStructures.Heap;
using PathfindingLib.API.DataStructures.PriorityQueue;
using System;

namespace PathfindingLib.DataStructures.Heap
{
	public class HeapNode<TContent, TKey> : IHeapNode<TContent, TKey>
		where TKey : IComparable
	{
		public HeapNode()
		{

		}

		public HeapNode(TContent content, TKey value)
		{
			this.Content = content;
			this.Key = value;
		}

		public TContent Content { get; set; }

		public TKey Key { get; set; }
	}
}
