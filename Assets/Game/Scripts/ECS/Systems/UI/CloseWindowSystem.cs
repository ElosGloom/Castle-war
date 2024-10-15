using FPS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems.UI
{
	public class CloseWindowSystem : IEcsRunSystem
	{
		private EcsFilterInject<Inc<WindowComponent, CloseWindowRequest>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter.Value)
			{
				ref var windowComponent = ref _filter.Pools.Inc1.Get(entity);
				windowComponent.WindowCloseCallback?.Invoke();
				FPS.UIService.Hide<UIWindow>(windowComponent.WindowType);

				systems.GetWorld().DelEntity(entity);
			}
		}
	}
}