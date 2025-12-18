using TMPro;
using UnityEngine;

namespace UI
{
	public class UIInventoryCellView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _counter;

		public void SetCount(string count) => _counter.text = count;
	}
}