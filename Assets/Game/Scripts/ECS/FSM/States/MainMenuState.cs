using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.FSM
{
	public class MainMenuState : IEcsSystem, IStateEnter, IStateExit
	{
		private readonly EcsWorldInject _world;
		
		
		public AppState TargetState => AppState.MainMenu;

		public void Enter()
		{
			UIHelper.ShowWindow<UIMainMenuWindow>(_world.Value);
		}

		public void Exit()
		{
			UIHelper.HideWindow<UIMainMenuWindow>(_world.Value);
		}
	}
}