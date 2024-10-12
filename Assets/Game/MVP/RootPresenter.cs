using MainMenu;

namespace MVP
{
    public sealed class RootPresenter : Presenter
    {
        public RootPresenter()
        {
            Init();
        }

        protected override void Init()
        {
            var main = Add<MainMenuPresenter>();
        }
    }
}