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
		[SerializeField] private SerializableDictionary<string, UIInventoryCellView> _armyCells;
		[SerializeField, Get] private ButtonsProvider _buttonsProvider;
		[SerializeField, Get] private StringButtonsProvider _stringButtonsProvider;

		public IButtonsProvider ButtonsProvider => _buttonsProvider;
		public IButtonsProvider<string> StringButtonsProvider => _stringButtonsProvider;


		private readonly CompositeDisposable _disposable = new();

		public void BindArmy(ReactiveDictionary<string, int> army)
		{
			//force update cells
			army.ObserveReplace().Subscribe(UpdateCounter).AddTo(_disposable);
		}

		void UpdateCounter(DictionaryReplaceEvent<string, int> replaceProtocol)
		{
			if (_armyCells.TryGetValue(replaceProtocol.Key, out var value))
				value.SetCount(replaceProtocol.NewValue.ToString());
		}

		protected override void AfterHide()
		{
			_disposable.Clear();
			base.AfterHide();
		}
	}
}