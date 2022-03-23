using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.API.DataStructures.PriorityQueue
{
	public interface IPriorityQueueWithKeyUpdate<TQueueItem, TContent, TKey> : IPriorityQueue<TQueueItem, TContent, TKey> 
		where TQueueItem : IQueueItem<TContent, TKey>, new()
		where TKey : IComparable
	{
		void UpdateKey(TContent content, TKey newKey);
	}
}
