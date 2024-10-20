using Common;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
	public class SaveSystem : IEcsPostDestroySystem, IEcsRunSystem
	{
		private EcsCustomInject<User> _user;

		public void PostDestroy(IEcsSystems systems)
		{
			var encoded = _user.Value.Serialize();
		}

		public void Run(IEcsSystems systems)
		{
			_user.Value.Playtime += Time.deltaTime;
		}
	}
}