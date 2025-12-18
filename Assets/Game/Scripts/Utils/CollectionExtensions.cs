using System.Collections.Generic;

namespace Utils
{
	public static class CollectionExtensions
	{
		public static void ReplaceAll<T>(
			this ICollection<T> target,
			IEnumerable<T> source)
		{
			if (target == null || source == null)
				return;
            
			if (target is List<T> list)
			{
				list.Clear();
				if (source is List<T> sourceList)
					list.AddRange(sourceList);
				else
					list.AddRange(source);

				return;
			}
            
			target.Clear();
			foreach (var item in source) 
				target.Add(item);
		}
	}
}