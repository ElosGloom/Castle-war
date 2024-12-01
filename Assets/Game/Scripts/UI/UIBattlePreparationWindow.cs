using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using FPS.UI.Buttons.Generic;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UIBattlePreparationWindow : UIWindow
    {
        public SerializableDictionary<string,UIUnitsCounter> counters;
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