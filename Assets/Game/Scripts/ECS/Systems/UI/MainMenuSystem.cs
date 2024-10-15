using Battle;
using Common;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.Systems.UI
{
	public class MainMenuSystem : BaseUIWindowSystem<UIMainMenuWindow>
	{
		private EcsWorldInject _world;
		private EcsCustomInject<User> _user;

		protected override void OnShow(UIMainMenuWindow window, int entity)
		{
			// ref var windowComponent = ref _filter.Pools.Inc1.Get(entity);
			window.ButtonsProvider.Subscribe("Start", () =>
			{
				UIHelper.ShowWindow<UIBattlePreparationWindow>(_world.Value);
				UIHelper.HideWindow<UIMainMenuWindow>(_world.Value);
				BattleFactory.SetupScene(_user.Value.CurrentLevel);
			});
		}
	}
}