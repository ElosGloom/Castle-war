using Common;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Network;
using UnityEngine;

namespace ECS.Systems
{
	public class SaveSystem : IEcsPostDestroySystem, IEcsRunSystem
	{
		private EcsCustomInject<User> _user;
		private EcsCustomInject<ApiService> _apiService;

		public void PostDestroy(IEcsSystems systems)
		{
			_apiService.Value.SyncUserData(_user.Value).Forget();
		}

		public void Run(IEcsSystems systems)
		{
			_user.Value.Playtime += Time.deltaTime;
		}
	}
}