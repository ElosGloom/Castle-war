using System.Collections.Generic;
using ECS.Monobehaviours;
using UniRx;

namespace Common
{
    public class RuntimeData
    {
        public ReactiveProperty<int> AvailableMeleeUnits;
        public ReactiveProperty<int> AvailableRangeUnits;
        public readonly ReactiveProperty<string> SelectedUnitsKey = new ();
        public Stack<UnitView> SpawnedUnits = new();
    }
}