using System.Collections.Generic;
using ECS.Monobehaviours;
using JetBrains.Collections.Viewable;

namespace Common
{
	public class RuntimeData
	{
		public readonly IViewableMap<string, int> DrawableUnits = new ViewableMap<string, int>();
		public readonly Stack<UnitView> SpawnedUnits = new();
		public string SelectedUnitKey;

		public void AddAvailableUnit(string unitKey) => DrawableUnits[unitKey]++;

		public void DeleteAvailableUnit(string unitKey)
		{
			DrawableUnits.TryGetValue(unitKey, out int value);
			if (value >= 0) DrawableUnits[unitKey]--;
		}
	}
}