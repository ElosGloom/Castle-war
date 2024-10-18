using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UIBattlePreparationWindow : UIWindow
    {
        public TMP_Text meleeUnitsCount;

        [SerializeField, Get] private ButtonsProvider buttonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;

        public readonly CompositeDisposable Disposable = new();

        protected override void AfterHide()
        {
            Disposable.Clear();
            base.AfterHide();
        }
    }
}