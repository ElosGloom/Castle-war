using Common;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Network
{
	public class ApiTestSystem : IEcsRunSystem
	{
		private const string Nickname = "cwuser";
		private const string Password = "cwpass";
		private readonly EcsCustomInject<ApiService> _apiService;
		private readonly EcsCustomInject<User> _user;

		public void Run(IEcsSystems systems)
		{
			if (Input.GetKeyDown(KeyCode.R))
				_apiService.Value.Register(Nickname, Password).Forget();
			if (Input.GetKeyDown(KeyCode.L))
				_apiService.Value.Login(Nickname, Password).Forget();
			if (Input.GetKeyDown(KeyCode.S))
				_apiService.Value.SyncUserData(_user.Value).Forget();
		}
	}
}