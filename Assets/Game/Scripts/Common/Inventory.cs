using System.Collections.Generic;
using JetBrains.Collections.Viewable;
using Utils;

namespace Common
{
	public class Inventory
	{
		public IViewableMap<string, int> AllItems => _container;

		private readonly ViewableMap<string, int> _container = new();


		public bool HasItem(string key, out int count)
		{
			var hasItem = _container.ContainsKey(key);
			count = hasItem ? _container[key] : default;
			return hasItem;
		}

		public int GetItemCount(string key) => _container.TryGetValue(key, out int count) ? count : default;

		public void AddItem(string key, int count = 1)
		{
			_container.TryAdd(key, default);
			_container[key] += count;
		}

		public bool TryConsumeItem(string key, int count)
		{
			if (_container.ContainsKey(key))
				return false;

			if (_container[key] - count <= 0)
				return false;

			_container[key] -= count;

			if (_container[key] == 0)
				_container.Remove(key);

			return true;
		}

		public void Copy(Inventory source) => Copy(source._container);
		private void Copy(IDictionary<string, int> source) => _container.ReplaceAll(source);
		public void Copy(Dictionary<string, int> source) => _container.ReplaceAll(source);
	}
}