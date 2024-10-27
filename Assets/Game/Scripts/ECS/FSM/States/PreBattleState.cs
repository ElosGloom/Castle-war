using Battle;
using Common;
using DTO;
using ECS.Monobehaviours;
using FPS.Pool;
using FPS.Sheets;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;
using UnityEngine;

namespace ECS.FSM
{
    public class PreBattleState : IEcsSystem, IState
    {
        private EcsWorldInject _world;
        private EcsCustomInject<User> _user;
        private EcsCustomInject<DTOStorage> _dtoStorage;
        private EcsCustomInject<RuntimeData> _runtimeData;

        public void Enter()
        {
            _runtimeData.Value.AvailableMeleeUnits.Value=_user.Value.Inventory["melee"];
            UIHelper.ShowWindow<UIBattlePreparationWindow>(_world.Value);
            BattleFactory.SetupScene(_user.Value.CurrentLevel);
            SpawnEnemyUnits();
        }

        private void SpawnEnemyUnits()
        {
            var lvlDto = _dtoStorage.Value.GetSingle<LevelDTO>();
            foreach (var unitDto in lvlDto.UnitsData)
            {
                var unit =FluffyPool.Get<UnitView>("enemyMelee");
                unit.transform.position = unitDto.Position;
                unit.transform.rotation= Quaternion.Euler(unitDto.Rotation);
                unit.type = unitDto.Type;
            }
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}