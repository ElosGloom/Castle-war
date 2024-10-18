using System.Collections.Generic;
using Game.Scripts.ECS.Monobehaviours;
using UniRx;

namespace Common
{
    public class RuntimeData
    {
        public ReactiveProperty<int> AvailableMeleeUnits;
        public Stack<UnitView> SpawnedUnits = new();
    }
}