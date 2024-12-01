using Common;
using ECS.Monobehaviours;
using FPS.Pool;
using Leopotam.EcsLite;
using UniRx;
using VContainer;

namespace ECS.Systems
{
    public class UnitSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _ecsWorld;
        private readonly RuntimeData _runtimeData;
        private readonly IObjectPool _objectPool;
        private EcsFilter _filter;
        private string _selectedUnitName;

        [Inject]
        public UnitSpawnSystem(EcsWorld ecsWorld, RuntimeData runtimeData, IObjectPool objectPool)
        {
            _ecsWorld = ecsWorld;
            _runtimeData = runtimeData;
            _objectPool = objectPool;
        }

        public void Run(IEcsSystems systems)
        {
            _filter = _ecsWorld.Filter<UnitSpawnRequest>().End();
            foreach (var unitsEntity in _filter)
            {
                EcsPool<UnitComponent> pool = _ecsWorld.GetPool<UnitComponent>();
                ref UnitComponent unitComponent = ref pool.Add(unitsEntity);

                EcsPool<UnitSpawnRequest> pool2 = _ecsWorld.GetPool<UnitSpawnRequest>();

                var unit = _objectPool.Get<UnitView>(_selectedUnitName);
                unit.transform.position = pool2.Get(unitsEntity).Position;
                unitComponent.UnitView = unit;

                _runtimeData.SpawnedUnits.Push(unit);
                pool2.Del(unitsEntity);
                _runtimeData.DeleteAvailableUnit(_selectedUnitName);
            }

            _runtimeData.SelectedUnitsKey.Subscribe(key => { _selectedUnitName = key; });
        }
    }
}