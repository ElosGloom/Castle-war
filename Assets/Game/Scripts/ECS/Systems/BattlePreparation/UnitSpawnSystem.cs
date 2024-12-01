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
		private readonly User _user;
		private readonly RuntimeData _runtimeData;
		private readonly IObjectPool _pool;
		private EcsFilter _filter;

		
		[Inject]
		public UnitSpawnSystem(EcsWorld ecsWorld, User user,
			RuntimeData runtimeData, IObjectPool pool)
		{
			_ecsWorld = ecsWorld;
			_user = user;
			_runtimeData = runtimeData;
			_pool = pool;
		}

		public void Init(IEcsSystems systems)
		{
			_runtimeData.AvailableMeleeUnits = new(_user.Inventory["melee"]);
			_filter = _ecsWorld.Filter<UnitSpawnRequest>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var unitsEntity in _filter)
			{
				EcsPool<UnitComponent> pool = _ecsWorld.GetPool<UnitComponent>();
				ref UnitComponent unitComponent = ref pool.Add(unitsEntity);

				EcsPool<UnitSpawnRequest> pool2 = _ecsWorld.GetPool<UnitSpawnRequest>();

				var unit = _pool.Get<UnitView>("melee");
				unit.transform.position = pool2.Get(unitsEntity).Position;
				unitComponent.UnitView = unit;

				_runtimeData.SpawnedUnits.Push(unit);
				pool2.Del(unitsEntity);
				_runtimeData.AvailableMeleeUnits.Value--;
			}
		}
	}
}