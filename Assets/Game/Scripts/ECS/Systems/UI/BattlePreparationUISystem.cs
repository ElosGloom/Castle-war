using Common;
using FPS.Pool;
using FPS.UI;
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
            var availableUnits = _runtimeData.Value.AvailableUnits;

            foreach (var counter in window.counters)
            {
                availableUnits.TryGetValue(counter.Key, out int value);
                UpdateCounter(new DictionaryReplaceEvent<string, int>(counter.Key, value, value));
            }

            availableUnits.ObserveReplace().Subscribe(UpdateCounter).AddTo(window.Disposable);

            window.ButtonsProvider.Subscribe("RestartDrawing", () =>
            {
                EcsPool<UnitComponent> pool = _world.Value.GetPool<UnitComponent>();
                _filter = _world.Value.Filter<UnitComponent>().End();

                foreach (var unitsEntity in _filter)
                {
                    ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
                    FluffyPool.Return(unitComponent.UnitView);
                    pool.Del(unitsEntity);
                    _runtimeData.Value.AddAvailableUnit(_runtimeData.Value.SpawnedUnits.Peek().type);
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
                    if (_runtimeData.Value.SpawnedUnits.Peek() == unitComponent.UnitView)
                    {
                        FluffyPool.Return(unitComponent.UnitView);
                        pool.Del(unitsEntity);

                        _runtimeData.Value.AddAvailableUnit(_runtimeData.Value.SpawnedUnits.Peek().type);
                        _runtimeData.Value.SpawnedUnits.Pop();
                    }
                }
            });

            window.StringButtonsProvider.Subscribe(OnClick);
            return;

            void UpdateCounter(DictionaryReplaceEvent<string, int> replaceProtocol)
            {
                if (window.counters.TryGetValue(replaceProtocol.Key, out var value))
                    value.unitsCount.text = replaceProtocol.NewValue.ToString();
            }
        }


        private void OnClick(string s)
        {
            _runtimeData.Value.SelectedUnitsKey.Value = s;
        }
    }
}