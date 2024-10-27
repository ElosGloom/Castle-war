using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.FSM
{
    public class LoginState : IEcsSystem, IState
    {
        private readonly EcsWorldInject _world;
        public void Enter()
        {
            UIHelper.ShowWindow<UILoginWindow>(_world.Value);
        }

        public void Update()
        {
        }

        public void Exit()
        {
            UIHelper.HideWindow<UILoginWindow>(_world.Value);
        }
    }
}