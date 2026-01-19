using System;
using Buildings;
using CMS;
using Common;
using ECS.FSM;
using ECS.Systems;
using ECS.Systems.Common;
using ECS.Systems.Hub;
using ECS.Systems.Timer;
using ECS.Systems.UI;
using FPS;
using JetBrains.Lifetimes;
using Leopotam.EcsLite;
using Network;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Lifetime = VContainer.Lifetime;

namespace ECS
{
	public class EcsStartup : MonoBehaviour
	{
		[SerializeField, Get] private LifetimeScope _scope;
		[SerializeField] private AssetProvider _assetProvider;

		private readonly LifetimeDefinition _appDefinition = new ();
		private EcsSystems _systems;

		public void Start()
		{
			_scope.CreateChild(builder =>
			{
				builder.RegisterInstance(_appDefinition.Lifetime);
				builder.RegisterInstance(_assetProvider);
				builder.Register<RuntimeData>(Lifetime.Singleton);
				builder.Register<TimerInitializer>(Lifetime.Singleton);
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

				.Add(CreateSystem<AppInitState>())
				.Add(CreateSystem<HubState>())
				.Add(CreateSystem<PreBattleState>())
				.Add(CreateSystem<HubBuilder>())
				.Add(CreateSystem<IAppStateMachine>())

				#endregion

				#region UI

				.Add(CreateSystem<CloseWindowSystem>())
				.Add(CreateSystem<HubUISystem>())
				.Add(CreateSystem<LoginUISystem>())
				.Add(CreateSystem<BattlePreparationUISystem>())

				#endregion

				#region Hub
				.Add(CreateSystem<BuildingsLoadSystem>())
				.Add(CreateSystem<BuildingSpawnSystem>())
				#endregion

				#region PreBattle

				.Add(CreateSystem<DrawingSystem>())
				.Add(CreateSystem<UnitSpawnSystem>())

				#endregion

				#region Battle

				#endregion

				.Add(CreateSystem<TimerUpdateSystem>())
				.Add(CreateSystem<SaveSystem>())
				.Add(CreateSystem<RemoveRequestsSystem>())
				.Init();

			return;

			T CreateSystem<T>() where T : IEcsSystem
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
			_appDefinition.Terminate();
			_systems?.Destroy();
			_systems?.GetWorld()?.Destroy();
			_systems = null;
		}
	}
}