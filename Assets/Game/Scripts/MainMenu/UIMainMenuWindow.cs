using FPS;
using Game.UIService.Buttons;
using UnityEngine;

namespace MainMenu
{
    public class UIMainMenuWindow : UIWindow
    {
        [SerializeField, Get] private ButtonsProvider buttonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
    }
}