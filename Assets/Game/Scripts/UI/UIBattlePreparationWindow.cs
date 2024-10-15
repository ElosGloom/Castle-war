using FPS;
using Game.UIService.Buttons;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIBattlePreparationWindow : UIWindow
    {
        public TMP_Text meleeUnitsCount;
        [SerializeField, Get] private ButtonsProvider buttonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
    }
}