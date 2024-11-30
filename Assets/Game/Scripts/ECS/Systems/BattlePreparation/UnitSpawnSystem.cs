using Common;
using ECS.Monobehaviours;
using FPS.UI;
using Game.Scripts.PreBattle;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ECS.Systems
{
    public class UnitSpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsCustomInject<User> _user;
        private EcsCustomInject<RuntimeData> _runtimeData;
        private EcsFilter _filter;
        private string _selectedUnitName;

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _runtimeData.Value.AvailableMeleeUnits = new(_user.Value.Inventory["melee"]);
            _runtimeData.Value.AvailableRangeUnits = new(_user.Value.Inventory["range"]);
        }

        public void Run(IEcsSystems systems)
        {
            _filter = _ecsWorld.Filter<UnitSpawnRequest>().End();
            foreach (var unitsEntity in _filter)
            {
                EcsPool<UnitComponent> pool = _ecsWorld.GetPool<UnitComponent>();
                ref UnitComponent unitComponent = ref pool.Add(unitsEntity);

                EcsPool<UnitSpawnRequest> pool2 = _ecsWorld.GetPool<UnitSpawnRequest>();

                var unit = FPS.Pool.FluffyPool.Get<UnitView>(_selectedUnitName);
                unit.transform.position = pool2.Get(unitsEntity).Position;
                unitComponent.UnitView = unit;

                _runtimeData.Value.SpawnedUnits.Push(unit);
                pool2.Del(unitsEntity);
                
                if (_selectedUnitName == "melee")
                {
                    _runtimeData.Value.AvailableMeleeUnits.Value--;
                }
                else if (_selectedUnitName == "range")
                {
                    _runtimeData.Value.AvailableRangeUnits.Value--;
                }
            }

            _runtimeData.Value.SelectedUnitsKey.Subscribe(key => { _selectedUnitName = key; });
        }
    }
}