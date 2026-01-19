using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UILoginWindow: UIWindow
    {
        [SerializeField, Get] private ButtonsProvider buttonsProvider;
        public TMP_InputField loginInput;
        public TMP_InputField passwordInput;

        public IButtonsProvider ButtonsProvider => buttonsProvider;
    }
}