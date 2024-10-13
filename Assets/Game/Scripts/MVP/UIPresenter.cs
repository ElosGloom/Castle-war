using Cysharp.Threading.Tasks;
using FPS;

namespace MVP
{
    public abstract class UIPresenter<T> : Presenter where T : UIWindow
    {
        protected T Window;

        protected abstract void OnShow();

        protected override void Init()
        {
            ShowWindow().Forget();
        }

        private async UniTaskVoid ShowWindow()
        {
            Window = await FPS.UIService.Show<T>();
            OnShow();
        }

        protected override void Dispose()
        {
            base.Dispose();
            FPS.UIService.Hide<T>();
            Window = null;
        }
    }
}