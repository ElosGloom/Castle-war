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
            BattleFactory.SetupScene(User.CurrentLevel);
            Dispose();
        }
    }
}