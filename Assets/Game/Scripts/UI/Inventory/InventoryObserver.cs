using System.Collections.Generic;
using CMS;
using Common;
using FPS.Pool;
using JetBrains.Collections.Viewable;
using UnityEngine;
using VContainer;
using Lifetime = JetBrains.Lifetimes.Lifetime;

namespace UI
{
	public class InventoryObserver
	{
		private readonly IViewableMap<string, int> _categorizedInventory = new ViewableMap<string, int>();
		private readonly Dictionary<string, UIInventoryCellView> _inventoryCells = new();
		private readonly IObjectPool _objectPool;
		private readonly User _user;
		private readonly AssetProvider _assetProvider;

		[Inject]
		public InventoryObserver(IObjectPool objectPool, User user, AssetProvider assetProvider)
		{
			_objectPool = objectPool;
			_user = user;
			_assetProvider = assetProvider;
		}

		public void Init(Lifetime lifetime, Transform parent, HashSet<string> category)
		{
			_categorizedInventory.Advise(lifetime, OnInventoryUpdate);
			Bind(category);
			lifetime.OnTermination(Clear);
			return;

			void OnInventoryUpdate(MapEvent<string, int> mapEvent)
			{
				switch (mapEvent.Kind)
				{
					case AddUpdateRemove.Add:
						var cell = _objectPool.Get<UIInventoryCellView>();
						//todo: update icon
						cell.Icon = _assetProvider.Sprites[mapEvent.Key];
						cell.transform.SetParent(parent, false);
						_inventoryCells.Add(mapEvent.Key, cell);
						break;

					case AddUpdateRemove.Update:
						_inventoryCells[mapEvent.Key].Count = mapEvent.NewValue.ToString();
						break;

					case AddUpdateRemove.Remove:
						_objectPool.Return(_inventoryCells[mapEvent.Key]);
						_inventoryCells.Remove(mapEvent.Key);
						break;
				}
			}
		}

		private void Clear()
		{
			foreach (var cell in _inventoryCells.Values) 
				_objectPool.Return(cell);
		}


		private void Bind(HashSet<string> category)
		{
			foreach (var (itemId, count) in _user.Inventory.AllItems)
			{
				if (!category.Contains(itemId))
					continue;

				_categorizedInventory.Add(itemId, count);
			}
		}
	}
}