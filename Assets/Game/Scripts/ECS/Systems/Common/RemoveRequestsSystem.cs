using System.Collections.Generic;
using Leopotam.EcsLite;
using VContainer;

namespace ECS.Systems.Common
{
	public class RemoveRequestsSystem : IEcsRunSystem
	{
		private readonly EcsWorld _world;
		private readonly List<(EcsFilter Filter, IEcsPool Pool)> _requests = new();

		[Inject]
		public RemoveRequestsSystem(EcsWorld world)
		{
			_world = world;
			AddRequest<CreateRequest>();
			// AddRequest<CleanRequest>();
			AddRequest<ClickRequest>();
		}

		private void AddRequest<T>() where T : struct
		{
			var filter = _world.Filter<T>().End();
			var pool = _world.GetPool<T>();
			_requests.Add((filter, pool));
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var (filter, pool) in _requests)
			{
				foreach (var entity in filter)
				{
					pool.Del(entity);
				}
			}
		}
	}
}