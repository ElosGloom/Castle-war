using Battle;
using Common;
using DTO;
using ECS.Monobehaviours;
using FPS.Pool;
using FPS.Sheets;
using Leopotam.EcsLite;
using UI;
using UnityEngine;
using VContainer;

namespace ECS.FSM
{
	public class PreBattleState : IEcsSystem, IStateEnter
	{
		private readonly EcsWorld _world;
		private readonly User _user;
		private readonly DTOStorage _dtoStorage;
		private readonly RuntimeData _runtimeData;
		private readonly IObjectPool _pool;

		[Inject]
		public PreBattleState(EcsWorld world, User user, DTOStorage dtoStorage,
			RuntimeData runtimeData, IObjectPool pool)
		{
			_world = world;
			_user = user;
			_dtoStorage = dtoStorage;
			_runtimeData = runtimeData;
			_pool = pool;
		}

		public AppState TargetState => AppState.PreBattle;

		public void Enter()
		{
			_runtimeData.AvailableMeleeUnits.Value = _user.Inventory["melee"];
			UIHelper.ShowWindow<UIBattlePreparationWindow>(_world);
			BattleFactory.SetupScene(_user.CurrentLevel);
			SpawnEnemyUnits();
		}

		private void SpawnEnemyUnits()
		{
			var lvlDto = _dtoStorage.GetSingle<LevelDTO>();
			foreach (var unitDto in lvlDto.UnitsData)
			{
				var unit = _pool.Get<UnitView>("enemyMelee");
				unit.transform.position = unitDto.Position;
				unit.transform.rotation = Quaternion.Euler(unitDto.Rotation);
				unit.type = unitDto.Type;
			}
		}
	}
}