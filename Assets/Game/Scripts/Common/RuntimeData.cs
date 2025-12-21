using System.Collections.Generic;
using ECS.Monobehaviours;
using ObservableCollections;

namespace Common
{
	public class RuntimeData
	{
		public readonly ObservableDictionary<string, int> AvailableUnits = new();
		public readonly Stack<UnitView> SpawnedUnits = new();
		public string SelectedUnitKey;

		public void AddAvailableUnit(string unitKey) => AvailableUnits[unitKey]++;

		public void DeleteAvailableUnit(string unitKey)
		{
			AvailableUnits.TryGetValue(unitKey, out int value);
			if (value >= 0) AvailableUnits[unitKey]--;
		}
	}
}