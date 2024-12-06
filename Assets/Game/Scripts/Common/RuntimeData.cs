using System.Collections.Generic;
using ECS.Monobehaviours;
using UniRx;

namespace Common
{
    public class RuntimeData
    {
        public ReactiveDictionary<string, int> AvailableUnits = new();
        public readonly ReactiveProperty<string> SelectedUnitsKey = new();
        public readonly Stack<UnitView> SpawnedUnits = new();

        public void AddAvailableUnit(string unitKey)
        {
            AvailableUnits[unitKey]++;
        }

        public void DeleteAvailableUnit(string unitKey)
        {
            AvailableUnits.TryGetValue(unitKey, out int value);
            if (value >= 0)
            {
                AvailableUnits[unitKey]--;
            }
        }
    }
}