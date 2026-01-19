using Common;
using ECS.Monobehaviours;
using FPS.Pool;
using Leopotam.EcsLite;
using VContainer;

namespace ECS.Systems
{
	public class UnitSpawnSystem : IEcsRunSystem, IEcsInitSystem
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

		public void Init(IEcsSystems systems)
		{
			_filter = _ecsWorld.Filter<UnitSpawnRequest>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var unitsEntity in _filter)
			{
				var pool = _ecsWorld.GetPool<UnitComponent>();
				ref var unitComponent = ref pool.Add(unitsEntity);

				var requestPool = _ecsWorld.GetPool<UnitSpawnRequest>();

				var unit = _objectPool.Get<UnitView>(_runtimeData.SelectedUnitKey);
				unit.transform.position = requestPool.Get(unitsEntity).Position;
				unitComponent.UnitView = unit;

				_runtimeData.SpawnedUnits.Push(unit);
				requestPool.Del(unitsEntity);
				_runtimeData.DeleteAvailableUnit(_runtimeData.SelectedUnitKey);
			}
		}
	}
}