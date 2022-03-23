using PathfindingLib.API.DataStructures.Heap;
using System;
using System.Collections.Generic;

namespace PathfindingLib.DataStructures.Heap
{
	public class GenericBinaryHeapWithKeyUpdate<THeapNode, TContent, TKey> : GenericBinaryHeap<THeapNode, TContent, TKey>, IBinaryHeapWithKeyUpdate<THeapNode, TContent, TKey>
        where THeapNode : IHeapNode<TContent, TKey>, new()
        where TKey : IComparable
    {
        protected readonly Dictionary<TContent, int> heapIndexes = new Dictionary<TContent, int>();

		public GenericBinaryHeapWithKeyUpdate() : base()
		{

		}

        public GenericBinaryHeapWithKeyUpdate(int initialCapacity) : base(initialCapacity)
		{
            
		}

        public bool Contains(TContent content)
		{
            if (!heapIndexes.TryGetValue(content, out int index))
            {
                return false;
            }
            return true;
        }

        public void UpdateKey(TContent content, TKey newKey)
        {
            if (!heapIndexes.TryGetValue(content, out int index))
            {
                return;
            }

            var oldKey = this.heapArray[index].Key;
            int comp = newKey.CompareTo(oldKey);
            this.heapArray[index].Key = newKey;
            if (comp < 0)
            {
                // decrease-key
                BubbleUp(index);
            }
            else if (comp > 0)
            {
                // increase-key
                BubbleDown(index);
            }
        }
        public override void Insert(TContent content, TKey key)
        {
            this.heapIndexes.Add(content, this.countCache);
            base.Insert(content, key);
        }

        public override THeapNode ExtractMin()
        {
            var min = base.ExtractMin();
            this.heapIndexes.Remove(min.Content);
            return min;
        }

        protected override void Swap(int i, int j)
        {
            heapIndexes[this.heapArray[i].Content] = j;
            heapIndexes[this.heapArray[j].Content] = i;
            base.Swap(i, j);
        }

		public override void Clear(int newInitialCapacity = 0)
		{
            this.heapIndexes.Clear();
			base.Clear(newInitialCapacity);
		}
	}
}
