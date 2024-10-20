using Common;
using ECS.FSM;
using ECS.Systems;
using ECS.Systems.UI;
using FPS.Sheets;
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

				#region States

				.Add(new AppInitState())
				.Add(new MainMenuState())
				.Add(new PreBattleState())
				.Add(new AppStateMachine())

				#endregion

				#region UI

				.Add(new CloseWindowSystem())
				.Add(new MainMenuSystem())
				.Add(new BattlePreparationUISystem())

				#endregion

				#region PreBattle

				.Add(new DrawingSystem())
				.Add(new UnitSpawnSystem())

				#endregion

				#region Battle

				#endregion

				.Add(new SaveSystem())
				.Inject(
					new RuntimeData(),
					new User(),
					new DTOStorage())
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