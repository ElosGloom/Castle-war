using Buildings;
using Common;
using DTO;
using ECS.FSM;
using FPS.Sheets;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace ECS.Systems.Hub
{
	public class BuildingsLoadSystem : IStateEnter, IEcsSystem
	{
		private readonly User _user;
		private readonly DTOStorage _dtoStorage;
		private readonly EcsWorld _world;
		public AppState TargetState => AppState.Hub;
		
		
		[Inject]
		public BuildingsLoadSystem(User user, DTOStorage dtoStorage, EcsWorld world)
		{
			_user = user;
			_dtoStorage = dtoStorage;
			_world = world;
		}

		public void Enter()
		{
			var categories = _dtoStorage.GetSingle<CategoryDTO>();
			foreach (var buildingId in categories.Buildings)
			{
				if (!_user.Inventory.HasItem(buildingId, out _))
					continue;

				var entity = _world.NewEntity();
				ref var buildingComponent = ref _world.GetPool<BuildingComponent>().Add(entity);
				buildingComponent.Id = buildingId;
				_world.GetPool<CreateRequest>().Add(entity);
			}
		}
	}
}