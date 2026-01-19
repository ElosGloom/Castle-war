using CMS;
using ECS;
using ECS.FSM;
using Leopotam.EcsLite;
using UnityEngine;

namespace Buildings
{
	public class CleanupSystem : IEcsRunSystem
	{
		private readonly EcsWorld _world;
		private readonly AssetProvider _assetProvider;
		private EcsFilter _filter;
		public AppState TargetState => AppState.Hub;

		public CleanupSystem(EcsWorld world, AssetProvider assetProvider)
		{
			_world = world;
			_assetProvider = assetProvider;
			_filter = world.Filter<CleanRequest>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				
			}
		}
	}
}