using System;
using FPS.UI;
using Leopotam.EcsLite;

namespace ECS
{
	public static class UIHelper
	{
		public static void ShowWindow<T>(EcsWorld world, Action closeCallback = null) where T : UIWindow
		{
			var entity = world.NewEntity();
			ref var windowComponent = ref world.GetPool<WindowComponent>().Add(entity);
			windowComponent.WindowCloseCallback = closeCallback;
			windowComponent.WindowType = typeof(T);
			world.GetPool<OpenWindowRequest<T>>().Add(entity);
		}

		public static void HideWindow<T>(EcsWorld world) where T : IWindow
		{
			int windowEntity = -1;
			Type type = typeof(T);
			var pool = world.GetPool<WindowComponent>();
			foreach (var entity in world.Filter<WindowComponent>().End())
			{
				var windowComponent = pool.Get(entity); //ref test
				if (windowComponent.WindowType != type)
					continue;

				windowEntity = entity;
				break;
			}

			world.GetPool<CloseWindowRequest>().Add(windowEntity);
		}
	}
}