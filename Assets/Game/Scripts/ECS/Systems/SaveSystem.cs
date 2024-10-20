using Common;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
	public class SaveSystem : IEcsPostDestroySystem
	{
		private EcsCustomInject<User> _user;

		public void PostDestroy(IEcsSystems systems)
		{
			var encoded = _user.Value.Serialize();
			PlayerPrefs.SetString(Constants.UserPrefsKey, encoded);
			PlayerPrefs.Save();
		}
	}
}