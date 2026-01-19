using CMS;
using ECS;
using ECS.FSM;
using ECS.Systems.Timer;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace Buildings
{
	public class BuildingSpawnSystem : IStateUpdate, IEcsSystem
	{
		private readonly EcsWorld _world;
		private readonly AssetProvider _assetProvider;
		private readonly TimerInitializer _timerInitializer;
		private EcsFilter _filter;
		public AppState TargetState => AppState.Hub;

		[Inject]
		public BuildingSpawnSystem(EcsWorld world, AssetProvider assetProvider, TimerInitializer timerInitializer)
		{
			_world = world;
			_assetProvider = assetProvider;
			_timerInitializer = timerInitializer;
			_filter = world.Filter<BuildingComponent>().Inc<CreateRequest>().Exc<TimerComponent>().End();
		}

		public void Update()
		{
			foreach (var entity in _filter)
			{
				ref var buildingComponent = ref _world.GetPool<BuildingComponent>().Get(entity);

				var viewInstance = Object.Instantiate(_assetProvider.Prefabs.GetBuilding(buildingComponent.Id));
				_world.GetPool<MonoView<BuildingView>>().Add(entity).View = viewInstance; //todo: check ref
				// _timerInitializer.CreateBuildingTimer(entity);
			}
		}
	}
}