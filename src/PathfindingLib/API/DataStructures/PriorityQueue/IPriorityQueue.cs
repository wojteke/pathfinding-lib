using System;

namespace PathfindingLib.API.DataStructures.PriorityQueue
{
	public interface IPriorityQueue<TQueueItem, TContent, TKey> 
		where TQueueItem : IQueueItem<TContent, TKey>, new()
		where TKey : IComparable
	{
		void Insert(TContent content, TKey priority);

		TQueueItem ExtractMin();

		void Clear(int newInitialCapacity = 0);
	}
}
