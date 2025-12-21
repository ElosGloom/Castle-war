using System.Collections.Generic;
using FPS;
using FPS.UI;
using FPS.UI.Buttons;
using FPS.UI.Buttons.Generic;
using ObservableCollections;
using R3;
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

		public void BindArmy(ObservableDictionary<string, int> army)
		{
			_disposable.ToObservable().Subscribe(_ => Debug.LogError(123));
			//force update cells
			army.ObserveReplace().Subscribe(@event =>
				UpdateCounter(@event.NewValue.Key, @event.NewValue.Value)).AddTo(_disposable);
			
			army.ObserveChanged().Subscribe(@event =>
				UpdateCounter(@event.NewItem.Key, @event.NewItem.Value)).AddTo(_disposable);
		}

		private void UpdateCounter(string key, int value)
		{
			if (_armyCells.TryGetValue(key, out var cell))
				cell.SetCount(value.ToString());
		}

		protected override void AfterHide()
		{
			_disposable.Clear();
			base.AfterHide();
		}
	}
}