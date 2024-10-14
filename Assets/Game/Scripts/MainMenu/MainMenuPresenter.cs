using Battle;
using Common;
using MVP;

namespace MainMenu
{
    public class MainMenuPresenter : UIPresenter<UIMainMenuWindow>
    {
        protected override void OnShow()
        {
            Window.ButtonsProvider.Subscribe("Start", OnStartClick);
        }

        private void OnStartClick()
        {
            var go = BattleFactory.SetupScene(User.CurrentLevel);
            Dispose();
        }
    }
}