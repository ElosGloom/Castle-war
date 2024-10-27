using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.FSM
{
    public class MainMenuState : IEcsSystem, IState
    {
        private readonly EcsWorldInject _world;

        public void Enter()
        {
            UIHelper.ShowWindow<UIMainMenuWindow>(_world.Value);
        }

        public void Update()
        {
        }

        public void Exit()
        {
            UIHelper.HideWindow<UIMainMenuWindow>(_world.Value);
        }
    }
}