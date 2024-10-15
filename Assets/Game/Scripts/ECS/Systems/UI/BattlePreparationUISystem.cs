using Common;
using Leopotam.EcsLite.Di;
using UI;

namespace ECS.Systems.UI
{
    public class BattlePreparationUISystem : BaseUIWindowSystem<UIBattlePreparationWindow>
    {
        private EcsWorldInject _world;
        private EcsCustomInject<User> _user;

        protected override void OnShow(UIBattlePreparationWindow window, int entity)
        {
            _user.Value.Load();
            window.meleeUnitsCount.text = _user.Value.Inventory["melee"].ToString();
;
            window.ButtonsProvider.Subscribe("RestartDrawing", () =>
            {
            });
        }
    }
}