using Leopotam.EcsLite;
using UI;
using VContainer;

namespace ECS.FSM
{
	public class MainMenuState : IEcsSystem, IStateEnter, IStateExit
	{
		private readonly EcsWorld _world;

		[Inject]
		public MainMenuState(EcsWorld world)
		{
			_world = world;
		}

		public AppState TargetState => AppState.MainMenu;

		public void Enter()
		{
			UIHelper.ShowWindow<UIMainMenuWindow>(_world);
		}

		public void Exit()
		{
			UIHelper.HideWindow<UIMainMenuWindow>(_world);
		}
	}
}