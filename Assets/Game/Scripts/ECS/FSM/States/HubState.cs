using Leopotam.EcsLite;
using UI;
using VContainer;

namespace ECS.FSM
{
	public class HubState : IEcsSystem, IStateEnter, IStateExit
	{
		private readonly EcsWorld _world;

		[Inject]
		public HubState(EcsWorld world)
		{
			_world = world;
		}

		public AppState TargetState => AppState.Hub;

		public void Enter()
		{
			UIHelper.ShowWindow<UIHubWindow>(_world);
		}

		public void Exit()
		{
			UIHelper.HideWindow<UIHubWindow>(_world);
		}
	}
}