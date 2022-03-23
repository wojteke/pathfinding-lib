using System;

namespace PathfindingLib.API.DataStructures.Heap
{
	public interface IBinaryHeapWithKeyUpdate<THeapNode, TContent, TKey> : IBinaryHeap<THeapNode, TContent, TKey> 
		where THeapNode : IHeapNode<TContent, TKey>, new()
		where TKey : IComparable
	{
		void UpdateKey(TContent content, TKey newKey);
	}
}
