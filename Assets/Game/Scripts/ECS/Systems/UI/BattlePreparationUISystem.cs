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
		private readonly RuntimeData _runtimeData;
		private readonly EcsWorld _world;
		private readonly IObjectPool _objectPool;

		[Inject]
		public BattlePreparationUISystem(IUIService uiService,
			EcsWorld world,
			RuntimeData runtimeData,
			IObjectPool objectPool) : base(uiService)
		{
			_world = world;
			_runtimeData = runtimeData;
			_objectPool = objectPool;
		}


		protected override void OnShow(UIBattlePreparationWindow window, int entity)
		{
			var availableUnits = _runtimeData.AvailableUnits;
			window.BindArmy(_runtimeData.AvailableUnits);
			
			window.ButtonsProvider.Subscribe("RestartDrawing", () =>
			{
				EcsPool<UnitComponent> pool = _world.GetPool<UnitComponent>();
				_filter = _world.Filter<UnitComponent>().End();

				foreach (var unitsEntity in _filter)
				{
					ref UnitComponent unitComponent = ref pool.Get(unitsEntity);
					_objectPool.Return(unitComponent.UnitView);
					pool.Del(unitsEntity);
					_runtimeData.AddAvailableUnit(_runtimeData.SpawnedUnits.Peek().type);
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
					if (_runtimeData.SpawnedUnits.Peek() == unitComponent.UnitView)
					{
						_objectPool.Return(unitComponent.UnitView);
						pool.Del(unitsEntity);

						_runtimeData.AddAvailableUnit(_runtimeData.SpawnedUnits.Peek().type);
						_runtimeData.SpawnedUnits.Pop();
					}
				}
			});

			window.StringButtonsProvider.Subscribe(OnClick);
		}


		private void OnClick(string s)
		{
			_runtimeData.SelectedUnitsKey.Value = s;
		}
	}
}