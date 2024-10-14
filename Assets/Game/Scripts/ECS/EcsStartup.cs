using ECS.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS
{
    public class EcsStartup : MonoBehaviour
    {
        EcsSystems _systems;


        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            _systems?.Destroy();
            _systems?.GetWorld()?.Destroy();
            _systems = null;
        }

        public void Init()
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);

            _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Add(new UnitSpawnSystem())
                
                .Inject()
                .Init();

            enabled = true;
        }
    }
}