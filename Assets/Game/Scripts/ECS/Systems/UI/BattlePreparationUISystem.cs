using Common;
using FPS.Pool;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;
using UniRx;

namespace ECS.Systems.UI
{
    public class BattlePreparationUISystem : BaseUIWindowSystem<UIBattlePreparationWindow>
    {
        private EcsFilter _filter;
        private EcsWorldInject _world;
        private EcsCustomInject<User> _user;
        private EcsCustomInject<RuntimeData> _runtimeData;


        protected override void OnShow(UIBattlePreparationWindow window, int entity)
        {
            _runtimeData.Value.AvailableMeleeUnits.Subscribe(count =>
            {
                window.meleeUnitsCount.text = count.ToString();
            }).AddTo(window.Disposable);

            window.ButtonsProvider.Subscribe("RestartDrawing", () =>
            {
                EcsPool<UnitComponent> pool = _world.Value.GetPool<UnitComponent>();
                _filter = _world.Value.Filter<UnitComponent>().End();

                foreach (var unitsEntity in _filter)
                {
                    ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
                    FluffyPool.Return(unitComponent.UnitView);
                    pool.Del(unitsEntity);
                    _runtimeData.Value.AvailableMeleeUnits.Value++;
                    _runtimeData.Value.SpawnedUnits.Pop();
                }
            });

            window.ButtonsProvider.Subscribe("ReturnUnit", () =>
            {
                EcsPool<UnitComponent> pool = _world.Value.GetPool<UnitComponent>();
                _filter = _world.Value.Filter<UnitComponent>().End();

                foreach (var unitsEntity in _filter)
                {
                    ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
                    if ( _runtimeData.Value.SpawnedUnits.Peek()==unitComponent.UnitView)
                    {
                        FluffyPool.Return(unitComponent.UnitView);
                        pool.Del(unitsEntity);
                        _runtimeData.Value.AvailableMeleeUnits.Value++;
                        _runtimeData.Value.SpawnedUnits.Pop();
                    }
                }
            });
        }
    }
}