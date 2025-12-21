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
		private readonly EcsWorld _ecsWorld;
		private readonly RuntimeData _runtimeData;
		private readonly IObjectPool _objectPool;
		private readonly DTOStorage _dtoStorage;
		private readonly User _user;
		private EcsFilter _filter;

		[Inject]
		public PreBattleState(EcsWorld ecsWorld, RuntimeData runtimeData, User user, IObjectPool objectPool, DTOStorage dtoStorage)
		{
			_ecsWorld = ecsWorld;
			_runtimeData = runtimeData;
			_objectPool = objectPool;
			_dtoStorage = dtoStorage;
			_user = user;
		}

		public AppState TargetState => AppState.PreBattle;

		public void Enter()
		{
			_runtimeData.AvailableUnits.Add("melee", _user.Inventory["melee"]);
			_runtimeData.AvailableUnits.Add("range", _user.Inventory["range"]);
			
			UIHelper.ShowWindow<UIBattlePreparationWindow>(_ecsWorld);
			BattleFactory.SetupScene(_user.CurrentLevel);
			SpawnEnemyUnits();
		}

		private void SpawnEnemyUnits()
		{
			var lvlDto = _dtoStorage.GetSingle<LevelDTO>();
			foreach (var unitDto in lvlDto.UnitsData)
			{
				var unit = _objectPool.Get<UnitView>("enemyMelee");
				unit.transform.position = unitDto.Position;
				unit.transform.rotation = Quaternion.Euler(unitDto.Rotation);
				unit.type = unitDto.Type;
			}
		}
	}
}