using System.Collections.Generic;
using ECS.Monobehaviours;
using UniRx;

namespace Common
{
    public class RuntimeData
    {
        public ReactiveProperty<int> AvailableMeleeUnits;
        public Stack<UnitView> SpawnedUnits = new();
    }
}