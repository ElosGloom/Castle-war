using System.Threading;
using Common;
using Cysharp.Threading.Tasks;
using ECS;
using ECS.FSM;
using FPS;
using Leopotam.EcsLite;
using Network;
using UI;

namespace Commands
{
	public class LoginCommand : AsyncCommand
	{
		private readonly ApiService _apiService;
		private readonly EcsWorld _world;
		private readonly User _user;


		public LoginCommand(ApiService apiService, EcsWorld world, User user)
		{
			_apiService = apiService;
			_world = world;
			_user = user;
		}

		public override async UniTask Do(CancellationToken token)
		{
			if (await _apiService.TryReLogin())
			{
				await _apiService.SyncUserData(_user);
				AppStateMachine.SetState(AppState.MainMenu);
			}
			else
				UIHelper.ShowWindow<UILoginWindow>(_world);
		}
	}
}