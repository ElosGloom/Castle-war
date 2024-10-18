using System;
using System.Collections.Generic;

namespace Common
{
	public class Inventory
	{
		public event Action<string, int> UpdateEvent;
		private readonly Dictionary<string, int> _container = new();

		public int this[string key]
		{
			get
			{
				_container.TryAdd(key, default);
				return _container[key];
			}
			set
			{
				_container.TryAdd(key, default);
				_container[key] = value;
				UpdateEvent?.Invoke(key, value);
			}
		}

		public void Clear() => _container.Clear();
		
		public void Copy(Inventory source) => Copy(source._container);

		public void Copy(Dictionary<string, int> source)
		{
			Clear();
			foreach (var kvp in source)
			{
				this[kvp.Key] = source[kvp.Key];
			}
		}
	}
}