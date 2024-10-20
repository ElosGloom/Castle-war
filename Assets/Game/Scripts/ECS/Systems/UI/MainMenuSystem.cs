using Common;
using ECS.FSM;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.Systems.UI
{
    public class MainMenuSystem : BaseUIWindowSystem<UIMainMenuWindow>
    {
        private EcsWorldInject _world;
        protected override void OnShow(UIMainMenuWindow window, int entity)
        {
            window.ButtonsProvider.Subscribe("Start", AppStateMachine.SetState<PreBattleState>);
        }
    }
}