using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.ECS
{
    public class EcsStartup : MonoBehaviour
    {
        
        [SerializeField] private SceneData sceneData;
        
        EcsSystems _systems;

        void Start()
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);

            _systems
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem ())
#endif
                .Init();
        }

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
    }
}
