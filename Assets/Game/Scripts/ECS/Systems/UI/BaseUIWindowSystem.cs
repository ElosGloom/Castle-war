using Cysharp.Threading.Tasks;
using FPS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems.UI
{
	public abstract class BaseUIWindowSystem<T> : IEcsRunSystem, IEcsInitSystem where T : UIWindow
	{
		private EcsFilter _filter;


		public void Init(IEcsSystems systems)
		{
			_filter = systems.GetWorld().Filter<WindowComponent>().Inc<OpenWindowRequest<T>>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				systems.GetWorld().GetPool<OpenWindowRequest<T>>().Del(entity);
				ShowWindow(entity).Forget();
			}
		}

		private async UniTaskVoid ShowWindow(int entity)
		{
			var window = await FPS.UIService.Show<T>();
			OnShow(window, entity);
		}

		protected abstract void OnShow(T window, int entity);
	}
}