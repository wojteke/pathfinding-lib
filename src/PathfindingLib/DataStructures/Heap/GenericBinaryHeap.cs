using PathfindingLib.API.DataStructures.Heap;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PathfindingLib.DataStructures.Heap
{

	public class GenericBinaryHeap<THeapNode, TContent, TKey> : IBinaryHeap<THeapNode, TContent, TKey>
		where THeapNode : IHeapNode<TContent, TKey>, new()
		where TKey : IComparable
	{
		protected IList<THeapNode> heapArray = new List<THeapNode>();

		protected int countCache = 0;

		public int Count => this.countCache;
		public GenericBinaryHeap()
		{

		}

		public GenericBinaryHeap(int initialCapacity)
		{
			this.heapArray = new List<THeapNode>(initialCapacity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Parent(int index)
		{
			return index < 1 ? 0 : (index - 1) >> 1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int LeftChild(int index)
		{
			return (index << 1) + 1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int RightChild(int index)
		{
			return (index << 1) + 2;
		}

		public virtual void Insert(TContent content, TKey key)
		{
			var node = new THeapNode();
			node.Content = content;
			node.Key = key;
			this.heapArray.Add(node);
			this.countCache++;
			this.BubbleUp(this.countCache - 1);
		}

		public virtual THeapNode ExtractMin()
		{
			var output = this.heapArray[0];
			Swap(0, this.countCache - 1);
			this.heapArray.RemoveAt(this.countCache - 1);
			this.countCache--;
			BubbleDown(0);
			return output;
		}

		protected virtual void Swap(int i, int j)
		{
			var temp = this.heapArray[i];
			this.heapArray[i] = this.heapArray[j];
			this.heapArray[j] = temp;
		}


		protected void BubbleUp(int index)
		{
			while (index > 0)
			{
				int parent = this.Parent(index);

				if (this.heapArray[parent].Key.CompareTo(this.heapArray[index].Key) <= 0) return;
				Swap(index, parent);
				index = parent;
			}
		}

		protected void BubbleDown(int index)
		{
			int lastParent = (this.countCache - 2) >> 1;
			while (index <= lastParent)
			{
				int left = (index << 1) + 1;
				int right = left + 1;
				int min = index;
				if (left < this.countCache && this.heapArray[left].Key.CompareTo(this.heapArray[min].Key) <= 0)
					min = left;
				if (right < this.countCache && this.heapArray[right].Key.CompareTo(this.heapArray[min].Key) <= 0)
					min = right;

				if (min == index) return;
				Swap(index, min);
				index = min;
			}
		}

		public virtual void Clear(int newInitialCapacity = 0)
		{
			this.heapArray = new List<THeapNode>(newInitialCapacity);
			countCache = 0;
		}
	}
}