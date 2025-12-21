using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using Utils;

namespace Common
{
	public class Inventory
	{
		private readonly ObservableDictionary<string, int> _container = new();

		public int this[string key]
		{
			get
			{
				if (!HasItem(key))
					_container.Add(key, default);

				return _container[key];
			}
			set
			{
				_container.TryAdd(key, default);
				_container[key] = value;
			}
		}

		public bool HasItem(string key) => _container.ContainsKey(key);

		public KeyValuePair<string, int>[] AllItems => _container.ToArray();

		public void Clear() => _container.Clear();

		public void Copy(Inventory source) => Copy(source._container);
		private void Copy(IDictionary<string, int> source) => _container.ReplaceAll(source);
		public void Copy(Dictionary<string, int> source) => _container.ReplaceAll(source);
	}
}