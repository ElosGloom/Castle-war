using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using FPS.UI.Buttons.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UIBattlePreparationWindow : UIWindow
    {
        public TMP_Text meleeUnitsCount;
        public TMP_Text rangeUnitsCount;

        [SerializeField, Get] private ButtonsProvider buttonsProvider;
        [SerializeField, Get] private StringButtonsProvider stringButtonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
        public IButtonsProvider<string> StringButtonsProvider => stringButtonsProvider;
        

        public readonly CompositeDisposable Disposable = new();

        protected override void AfterHide()
        {
            Disposable.Clear();
            base.AfterHide();
        }
    }
}