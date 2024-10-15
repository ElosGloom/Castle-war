using Game.Scripts.ECS.Monobehaviours;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace ECS.Systems
{
    public class UnitSpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private Camera _camera;
        private float _spawnDelay = 0.05f; 
        private float _lastSpawnTime = 0f;

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _camera = Camera.main;
        }

        public void Run(IEcsSystems systems)
        {
            if (!Input.GetMouseButton(0)) return;

            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            if (Time.time - _lastSpawnTime < _spawnDelay) return;

            var ray =_camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;

            var hitPoint = hit.point;

            if (!hit.collider.gameObject.GetComponent<SpawnZone>()) return;
            
            var newUnit = _ecsWorld.NewEntity();
            EcsPool<UnitComponent> pool = _ecsWorld.GetPool<UnitComponent>();

            var unit = FPS.Pool.FluffyPool.Get<UnitView>("melee");
            unit.transform.position = hitPoint;

            ref UnitComponent unitComponent = ref pool.Add(newUnit);
            unitComponent.UnitView = unit;

            _lastSpawnTime = Time.time;
        }
    }
}