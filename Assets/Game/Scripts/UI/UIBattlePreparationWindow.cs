using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using FPS.UI.Buttons.Generic;
using JetBrains.Collections.Viewable;
using UnityEngine;

namespace UI
{
	public class UIBattlePreparationWindow : UIWindow
	{
		[SerializeField] private SerializableDictionary<string, UIInventoryCellView> _armyCells;
		[SerializeField, Get] private ButtonsProvider _buttonsProvider;
		[SerializeField, Get] private StringButtonsProvider _stringButtonsProvider;

		public IButtonsProvider ButtonsProvider => _buttonsProvider;
		public IButtonsProvider<string> StringButtonsProvider => _stringButtonsProvider;



		public void BindArmy(IViewableMap<string, int> runtimeDataDrawableUnits)
		{
			
		}

		private void UpdateCounter(string key, int value)
		{
			if (_armyCells.TryGetValue(key, out var cell))
				cell.Count = value.ToString();
		}
	}
}