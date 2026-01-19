using FPS.UI;
using Leopotam.EcsLite;
using VContainer;

namespace ECS.Systems.UI
{
	public class CloseWindowSystem : IEcsRunSystem, IEcsInitSystem
	{
		private readonly IUIService _uiService;
		private EcsFilter _filter;

		[Inject]
		public CloseWindowSystem(IUIService uiService)
		{
			_uiService = uiService;
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var pool = systems.GetWorld().GetPool<WindowComponent>();
				ref var windowComponent = ref pool.Get(entity);
				windowComponent.WindowCloseCallback?.Invoke();
				_uiService.Hide(windowComponent.WindowType);

				systems.GetWorld().DelEntity(entity);
			}
		}

		public void Init(IEcsSystems systems)
		{
			_filter = systems.GetWorld().Filter<WindowComponent>().Inc<CloseWindowRequest>().End();
		}
	}
}