using MainMenu;

namespace MVP
{
    public sealed class RootPresenter : Presenter
    {
        private static bool _hasInstance;

        public static void Create()
        {
            if (_hasInstance)
                return;

            new RootPresenter().Init();
            _hasInstance = true;
        }

        private RootPresenter()
        {
        }

        protected override void Init()
        {
            var main = OpenPresenter<MainMenuPresenter>();
        }
    }
}