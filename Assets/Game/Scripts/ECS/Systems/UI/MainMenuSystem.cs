using ECS.FSM;
using FPS.UI;
using UI;
using VContainer;

namespace ECS.Systems.UI
{
	public class MainMenuSystem : BaseUIWindowSystem<UIMainMenuWindow>
	{
		[Inject]
		public MainMenuSystem(IUIService uiService) : base(uiService) { }

		protected override void OnShow(UIMainMenuWindow window, int entity)
		{
			window.ButtonsProvider.Subscribe("Start", () => AppStateMachine.SetState(AppState.PreBattle));
		}
	}
}