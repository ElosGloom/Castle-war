using Leopotam.EcsLite;
using VContainer;
using VContainer.Unity;

namespace ECS.Systems.Timer
{
	public class TimerInitializer
	{
		private readonly EcsWorld _world;

		public bool HasTimer(int entity) => Pool.Has(entity);
		private EcsPool<TimerComponent> Pool => _world.GetPool<TimerComponent>();

		[Inject]
		public TimerInitializer(EcsWorld world)
		{
			_world = world;
		}

		public void CreateBuildingTimer(int buildingId)
		{
			
		}
	}
}