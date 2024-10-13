using Game.Scripts.ECS.Components;
using Leopotam.EcsLite;

namespace Game.Scripts.ECS.Systems
{
    public class UnitSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;

        
        public void Run(IEcsSystems systems)
        {
            var newUnit = _ecsWorld.NewEntity();
            EcsPool<UnitComponent> pool = _ecsWorld.GetPool<UnitComponent> ();
            
            ref UnitComponent unitComponent = ref pool.Add (newUnit);

        }
    }
}