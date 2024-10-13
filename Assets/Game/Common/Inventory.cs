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
    }
}