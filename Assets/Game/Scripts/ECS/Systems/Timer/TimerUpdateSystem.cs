using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace ECS.Systems.Timer
{
	public class TimerUpdateSystem : IEcsRunSystem, IEcsInitSystem
	{
		private readonly EcsWorld _world;
		private EcsFilter _filter;

		[Inject]
		public TimerUpdateSystem(EcsWorld world)
		{
			_world = world;
		}

		public void Init(IEcsSystems systems)
		{
			_filter = _world.Filter<TimerComponent>().End();
		}

		public void Run(IEcsSystems systems)
		{
			var pool = _world.GetPool<TimerComponent>();
			foreach (var entity in _filter)
			{
				ref var timer = ref pool.Get(entity);
				timer.TimeLeft -= Time.deltaTime;

				if (timer.TimeLeft > 0)
					continue;

				timer.Callback?.Invoke();
				if (timer.Loop)
					timer.TimeLeft += timer.LoopTime;
				else
					systems.GetWorld().DelEntity(entity);
			}
		}
	}
}