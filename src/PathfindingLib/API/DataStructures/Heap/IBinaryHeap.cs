using System;

namespace PathfindingLib.API.DataStructures.Heap
{
	public interface IBinaryHeap<THeapNode, TContent, TKey> 
		where THeapNode : IHeapNode<TContent, TKey>, new()
		where TKey : IComparable
	{
		int Parent(int index);

		int LeftChild(int index);

		int RightChild(int index);

		void Insert(TContent content, TKey value);

		THeapNode ExtractMin();

		void Clear(int newInitialCapacity = 0);
	}
}
