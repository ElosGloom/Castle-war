using Common;
using Game.Scripts.PreBattle;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace ECS.Systems
{
    public class DrawingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsWorld _ecsWorld;
        private readonly RuntimeData _runtimeData;
        private Camera _camera;
        private float _spawnDelay = 0.05f;
        private float _lastSpawnTime = 0f;

        [Inject]
        public DrawingSystem(EcsWorld ecsWorld, RuntimeData runtimeData)
        {
            _ecsWorld = ecsWorld;
            _runtimeData = runtimeData;
        }

        public void Init(IEcsSystems systems)
        {
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

            if (_runtimeData.SelectedUnitsKey.Value == null) return;
            
            _runtimeData.AvailableUnits.TryGetValue(_runtimeData.SelectedUnitsKey.Value, out int value);
            if (value <= 0) return;

            var newUnit = _ecsWorld.NewEntity();
            EcsPool<UnitSpawnRequest> pool = _ecsWorld.GetPool<UnitSpawnRequest>();
            ref UnitSpawnRequest unitComponent = ref pool.Add(newUnit);
            unitComponent.Position = hitPoint;

            _lastSpawnTime = Time.time;
        }
    }
}