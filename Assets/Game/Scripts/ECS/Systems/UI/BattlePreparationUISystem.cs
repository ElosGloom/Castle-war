using Common;
using FPS.Pool;
using FPS.UI;
using Leopotam.EcsLite;
using UI;
using UniRx;
using VContainer;

namespace ECS.Systems.UI
{
    public class BattlePreparationUISystem : BaseUIWindowSystem<UIBattlePreparationWindow>
    {
        private EcsFilter _filter;
        private readonly EcsWorld _world;
        private readonly IObjectPool _pool;
        private readonly RuntimeData _runtimeData;

        [Inject]
        public BattlePreparationUISystem(IUIService uiService, EcsWorld world,
            RuntimeData runtimeData, IObjectPool pool) : base(uiService)
        {
            _world = world;
            _runtimeData = runtimeData;
            _pool = pool;
        }

        protected override void OnShow(UIBattlePreparationWindow window, int entity)
        {
            _runtimeData.AvailableMeleeUnits.Subscribe(count =>
            {
                window.meleeUnitsCount.text = count.ToString();
            }).AddTo(window.Disposable);

            window.ButtonsProvider.Subscribe("RestartDrawing", () =>
            {
                EcsPool<UnitComponent> pool = _world.GetPool<UnitComponent>();
                _filter = _world.Filter<UnitComponent>().End();

                foreach (var unitsEntity in _filter)
                {
                    ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
                    _pool.Return(unitComponent.UnitView);
                    pool.Del(unitsEntity);
                    _runtimeData.AvailableMeleeUnits.Value++;
                    _runtimeData.SpawnedUnits.Pop();
                }
            });

            window.ButtonsProvider.Subscribe("ReturnUnit", () =>
            {
                EcsPool<UnitComponent> pool = _world.GetPool<UnitComponent>();
                _filter = _world.Filter<UnitComponent>().End();

                foreach (var unitsEntity in _filter)
                {
                    ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
                    if ( _runtimeData.SpawnedUnits.Peek()==unitComponent.UnitView)
                    {
                        _pool.Return(unitComponent.UnitView);
                        pool.Del(unitsEntity);
                        _runtimeData.AvailableMeleeUnits.Value++;
                        _runtimeData.SpawnedUnits.Pop();
                    }
                }
            });
        }
    }
}