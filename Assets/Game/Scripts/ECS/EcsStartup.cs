using Common;
using ECS.Systems;
using ECS.Systems.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS
{
	public class EcsStartup : MonoBehaviour
	{
		private EcsSystems _systems;

		public void Start()
		{
			var world = new EcsWorld();
			_systems = new EcsSystems(world);
			_systems

				#region Debug

#if UNITY_EDITOR
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif

				#endregion

				.Add(new AppInitSystem())

				#region UI

				.Add(new CloseWindowSystem())
				.Add(new MainMenuSystem())
				.Add(new BattlePreparationUISystem())

				#endregion

				#region PreBattle

				.Add(new UnitSpawnSystem())

				#endregion

				#region Battle

				#endregion

				.Inject(
					new RuntimeTempData(),
					new User())
				.Init();
		}

		private void Update()
		{
			_systems?.Run();
		}

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems?.GetWorld()?.Destroy();
			_systems = null;
		}
	}
}