using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using UnityEngine;

namespace UI
{
	public class UIHubWindow : UIWindow
	{
		[SerializeField] private Transform _armyParent;
		[SerializeField] private Transform _currencyParent;

		[SerializeField, Get] private ButtonsProvider _buttonsProvider;


		public IButtonsProvider ButtonsProvider => _buttonsProvider;
		public Transform ArmyParent => _armyParent;
		public Transform CurrencyParent => _currencyParent;
	}
}