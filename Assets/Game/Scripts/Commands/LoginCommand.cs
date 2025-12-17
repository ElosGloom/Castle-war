using System.Threading;
using Common;
using Cysharp.Threading.Tasks;
using ECS;
using ECS.FSM;
using FPS;
using Leopotam.EcsLite;
using Network;
using UI;
using VContainer;

namespace Commands
{
	public class LoginCommand : AsyncCommand
	{
		private readonly ApiService _apiService;
		private readonly EcsWorld _world;
		private readonly User _user;
		private readonly IAppStateMachine _appStateMachine;

		[Inject]
		public LoginCommand(ApiService apiService, EcsWorld world, User user, IAppStateMachine appStateMachine)
		{
			_apiService = apiService;
			_world = world;
			_user = user;
			_appStateMachine = appStateMachine;
		}

		public override async UniTask Do(CancellationToken token)
		{
			if (await _apiService.TryReLogin())
			{
				await _apiService.SyncUserData(_user);
				_appStateMachine.SetState(AppState.Hub);
			}
			else
				UIHelper.ShowWindow<UILoginWindow>(_world);
		}
	}
}