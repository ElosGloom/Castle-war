using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIInventoryCellView : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _counter;

		public string Count
		{
			set => _counter.text = value;
		}

		public Sprite Icon
		{
			set => _icon.sprite = value;
		}
	}
}