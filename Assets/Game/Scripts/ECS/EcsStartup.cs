using System;
using CMS;
using Common;
using ECS.FSM;
using ECS.Systems;
using ECS.Systems.UI;
using FPS;
using Leopotam.EcsLite;
using Network;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ECS
{
	public class EcsStartup : MonoBehaviour
	{
		[SerializeField, Get] private LifetimeScope _scope;
		[SerializeField] private AssetProvider _assetProvider;

		private EcsSystems _systems;

		public void Start()
		{
			_scope.CreateChild(builder =>
			{
				builder.RegisterInstance(_assetProvider);
				builder.Register<RuntimeData>(Lifetime.Singleton);
				builder.Register<User>(Lifetime.Singleton);
				builder.Register<ApiService>(Lifetime.Singleton);
				builder.Register<AppStateMachine>(Lifetime.Singleton).As<IAppStateMachine>();
				builder.RegisterInstance<EcsWorld>(new());
				builder.RegisterBuildCallback(InitSystems);
			});
		}

		private void InitSystems(IObjectResolver resolver)
		{
			var world = resolver.Resolve<EcsWorld>();

			_systems = new EcsSystems(world);
			_systems

				#region Debug

#if UNITY_EDITOR
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif

				#endregion

				#region States

				.Add(NewSystem<AppInitState>())
				.Add(NewSystem<HubState>())
				.Add(NewSystem<PreBattleState>())
				.Add(NewSystem<HubBuilder>())
				.Add(NewSystem<IAppStateMachine>())

				#endregion

				#region UI

				.Add(NewSystem<CloseWindowSystem>())
				.Add(NewSystem<HubUISystem>())
				.Add(NewSystem<LoginUISystem>())
				.Add(NewSystem<BattlePreparationUISystem>())

				#endregion

				#region Hub

				#endregion

				#region PreBattle

				.Add(NewSystem<DrawingSystem>())
				.Add(NewSystem<UnitSpawnSystem>())

				#endregion

				#region Battle

				#endregion

				.Add(NewSystem<SaveSystem>())
				.Init();

			return;

			T NewSystem<T>() where T : IEcsSystem
			{
				return resolver.TryResolve<T>(out var resolved)
					? resolved
					: Activator.CreateInstance<T>();
			}
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