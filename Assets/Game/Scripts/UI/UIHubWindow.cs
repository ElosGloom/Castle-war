using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using UnityEngine;

namespace UI
{
    public class UIHubWindow : UIWindow
    {
        [SerializeField, Get] private ButtonsProvider _buttonsProvider;

        public IButtonsProvider ButtonsProvider => _buttonsProvider;
    }
}