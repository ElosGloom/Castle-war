using ECS.FSM;
using FPS.UI;
using UI;
using VContainer;

namespace ECS.Systems.UI
{
	public class HubUISystem : BaseUIWindowSystem<UIHubWindow>
	{
		private readonly IAppStateMachine _appStateMachine;

		[Inject]
		public HubUISystem(IUIService uiService, IAppStateMachine appStateMachine) : base(uiService)
		{
			_appStateMachine = appStateMachine;
		}

		protected override void OnShow(UIHubWindow window, int entity)
		{
			window.ButtonsProvider.Subscribe("Start", () => _appStateMachine.SetState(AppState.PreBattle));
		}
	}
}