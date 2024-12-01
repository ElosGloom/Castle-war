using Common;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Network;
using UnityEngine;
using VContainer;

namespace ECS.Systems
{
	public class SaveSystem : IEcsPostDestroySystem, IEcsRunSystem
	{
		private readonly User _user;
		private readonly ApiService _apiService;

		
		[Inject]
		public SaveSystem(User user, ApiService apiService)
		{
			_user = user;
			_apiService = apiService;
		}

		public void PostDestroy(IEcsSystems systems)
		{
			_apiService.SyncUserData(_user).Forget();
		}

		public void Run(IEcsSystems systems)
		{
			_user.Playtime += Time.deltaTime;
		}
	}
}