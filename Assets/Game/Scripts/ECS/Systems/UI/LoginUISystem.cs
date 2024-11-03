using Common;
using Cysharp.Threading.Tasks;
using ECS.FSM;
using Leopotam.EcsLite.Di;
using Network;
using UI;

namespace ECS.Systems.UI
{
	public class LoginUISystem : BaseUIWindowSystem<UILoginWindow>
	{
		private EcsCustomInject<User> _user;
		private EcsCustomInject<ApiService> _apiService;
		private EcsWorldInject _world;


		protected override void OnShow(UILoginWindow window, int entity)
		{
			window.ButtonsProvider.Subscribe("Close", () =>
			{
				AppStateMachine.SetState(AppState.MainMenu);
				UIHelper.HideWindow<UILoginWindow>(_world.Value);
			});

			window.ButtonsProvider.Subscribe("Login", () =>
				TryLogin(window.loginInput.text, window.passwordInput.text).Forget());

			window.ButtonsProvider.Subscribe("SignUp", () =>
				TrySignUp(window.loginInput.text, window.passwordInput.text).Forget());
		}

		private async UniTaskVoid TryLogin(string userName, string password)
		{
			var result = await _apiService.Value.Login(userName, password);
			HandleResponse(result).Forget();
		}

		private async UniTaskVoid TrySignUp(string userName, string password)
		{
			var result = await _apiService.Value.Register(userName, password);
			HandleResponse(result).Forget();
		}

		private async UniTaskVoid HandleResponse(RequestResult result)
		{
			if (result.IsSuccess)
			{
				await _apiService.Value.SyncUserData(_user.Value);
				AppStateMachine.SetState(AppState.MainMenu);
				UIHelper.HideWindow<UILoginWindow>(_world.Value);
			}
		}
	}
}