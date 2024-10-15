using FPS;
using Game.UIService.Buttons;
using UnityEngine;

namespace UI
{
    public class UIMainMenuWindow : UIWindow
    {
        [SerializeField, Get] private ButtonsProvider buttonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
    }
}