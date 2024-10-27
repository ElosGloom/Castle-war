using Common;
using ECS.Monobehaviours;
using Game.Scripts.PreBattle;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ECS.Systems
{
    public class DrawingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsCustomInject<User> _user;
        private EcsCustomInject<RuntimeData> _runtimeData;
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

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;

            var hitPoint = hit.point;

            if (!hit.collider.gameObject.GetComponent<SpawnZone>()) return;

            if (_runtimeData.Value.AvailableMeleeUnits.Value <= 0) return;

            var newUnit = _ecsWorld.NewEntity();
            EcsPool<UnitSpawnRequest> pool = _ecsWorld.GetPool<UnitSpawnRequest>();
            ref UnitSpawnRequest unitComponent = ref pool.Add(newUnit);
            unitComponent.Position = hitPoint;

            _lastSpawnTime = Time.time;
        }
    }
}