using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UILoginWindow: UIWindow
    {
        [SerializeField, Get] private ButtonsProvider buttonsProvider;
        public TMP_InputField loginInput;
        public TMP_InputField passwordInput;

        public IButtonsProvider ButtonsProvider => buttonsProvider;

        public readonly CompositeDisposable Disposable = new();

        protected override void AfterHide()
        {
            Disposable.Clear();
            base.AfterHide();
        }
    }
}