using Common;
using Cysharp.Threading.Tasks;
using ECS.FSM;
using FPS.UI;
using Leopotam.EcsLite;
using Network;
using UI;
using VContainer;

namespace ECS.Systems.UI
{
	public class LoginUISystem : BaseUIWindowSystem<UILoginWindow>
	{
		private readonly User _user;
		private readonly EcsWorld _world;
		private readonly ApiService _apiService;

		[Inject]
		public LoginUISystem(IUIService uiService, User user, ApiService apiService,
			EcsWorld world) : base(uiService)
		{
			_user = user;
			_world = world;
			_apiService = apiService;
		}


		protected override void OnShow(UILoginWindow window, int entity)
		{
			window.ButtonsProvider.Subscribe("Close", () =>
			{
				AppStateMachine.SetState(AppState.MainMenu);
				UIHelper.HideWindow<UILoginWindow>(_world);
			});

			window.ButtonsProvider.Subscribe("Login", () =>
				TryLogin(window.loginInput.text, window.passwordInput.text).Forget());

			window.ButtonsProvider.Subscribe("SignUp", () =>
				TrySignUp(window.loginInput.text, window.passwordInput.text).Forget());
		}

		private async UniTaskVoid TryLogin(string userName, string password)
		{
			var result = await _apiService.Login(userName, password);
			HandleResponse(result).Forget();
		}

		private async UniTaskVoid TrySignUp(string userName, string password)
		{
			var result = await _apiService.Register(userName, password);
			HandleResponse(result).Forget();
		}

		private async UniTaskVoid HandleResponse(RequestResult result)
		{
			if (result.IsSuccess)
			{
				await _apiService.SyncUserData(_user);
				AppStateMachine.SetState(AppState.MainMenu);
				UIHelper.HideWindow<UILoginWindow>(_world);
			}
		}
	}
}