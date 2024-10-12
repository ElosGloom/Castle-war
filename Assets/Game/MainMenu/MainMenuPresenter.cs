using Battle;
using MVP;

namespace MainMenu
{
    public class MainMenuPresenter : UIPresenter<MainMenuWindow>
    {
        protected override void OnShow()
        {
            Window.ButtonsProvider.Subscribe("Start", OnStartClick);
        }

        private void OnStartClick()
        {
            BattleFactory.SetupScene(1);
        }

    }
}