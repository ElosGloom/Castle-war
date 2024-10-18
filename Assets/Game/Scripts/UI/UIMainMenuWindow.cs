using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using UnityEngine;

namespace UI
{
    public class UIMainMenuWindow : UIWindow
    {
        [SerializeField, Get] private ButtonsProvider buttonsProvider;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
    }
}