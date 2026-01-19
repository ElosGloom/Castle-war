using Buildings;
using CMS;
using Common;
using DTO;
using ECS.FSM;
using ECS.Systems.Timer;
using FPS.Sheets;
using Leopotam.EcsLite;

namespace ECS.Systems.Hub
{
	public class ItemsHarvestSystem : IEcsSystem, IStateEnter, IStateUpdate
	{
		private readonly TimerInitializer _timerInitializer;
		private readonly User _user;
		private readonly EcsWorld _ecsWorld;
		private readonly AssetProvider _assetProvider;
		private readonly DTOStorage _dtoStorage;
		private EcsFilter _filter;
		public AppState TargetState => AppState.Hub;

		public ItemsHarvestSystem(TimerInitializer timerInitializer, User user,
			EcsWorld ecsWorld, AssetProvider assetProvider, DTOStorage dtoStorage)
		{
			_timerInitializer = timerInitializer;
			_user = user;
			_ecsWorld = ecsWorld;
			_assetProvider = assetProvider;
			_dtoStorage = dtoStorage;
		}

		public void Enter()
		{
			_filter = _ecsWorld.Filter<BuildingComponent>().Inc<ClickRequest>().End();
		}

		public void Update()
		{
			foreach (var entity in _filter)
			{
				var buildingsPool = _ecsWorld.GetPool<BuildingComponent>();
				ref var building = ref buildingsPool.Get(entity);

				var buildingDto = _dtoStorage.Get<BuildingDTO>(building.Id);
				_user.Inventory.AddItem(buildingDto.ItemId, building.CollectedItems);
				building.CollectedItems = 0;
				
				var timerPool = _ecsWorld.GetPool<TimerComponent>();
				if (timerPool.Has(entity))
					continue;

				ref var timer = ref timerPool.Add(entity);
			}
		}
	}
}