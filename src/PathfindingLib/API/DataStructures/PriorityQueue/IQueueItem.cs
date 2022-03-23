using System;

namespace PathfindingLib.API.DataStructures.PriorityQueue
{
	public interface IQueueItem<TContent, TKey> 
		where TKey : IComparable
	{
		TContent Content { get; set; }

		TKey Key { get; set; }
	}
}