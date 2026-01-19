using Common;
using ECS.FSM;
using FPS.Pool;
using FPS.UI;
using Leopotam.EcsLite;
using UI;
using VContainer;

namespace ECS.Systems.UI
{
	public class BattlePreparationUISystem : BaseUIWindowSystem<UIBattlePreparationWindow>
	{
		private EcsFilter _filter;
		private readonly RuntimeData _runtimeData;
		private readonly EcsWorld _world;
		private readonly IObjectPool _objectPool;
		private readonly IAppStateMachine _stateMachine;

		[Inject]
		public BattlePreparationUISystem(IUIService uiService,
			EcsWorld world,
			RuntimeData runtimeData,
			IObjectPool objectPool,
			IAppStateMachine stateMachine) : base(uiService)
		{
			_world = world;
			_runtimeData = runtimeData;
			_objectPool = objectPool;
			_stateMachine = stateMachine;
		}


		protected override void OnShow(UIBattlePreparationWindow window, int entity)
		{
			window.BindArmy(_runtimeData.DrawableUnits);

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

			window.ButtonsProvider.Subscribe("Home", () => _stateMachine.SetState(AppState.Hub));
			window.StringButtonsProvider.Subscribe(OnClick);
		}


		private void OnClick(string s)
		{
			_runtimeData.SelectedUnitKey = s;
		}
	}
}