using System;
using UnityEngine;

namespace Buildings
{
	public class BuildingView : MonoBehaviour
	{
		public event Action ClickEvent;

		private void OnMouseDown()
		{
			ClickEvent?.Invoke();
		}
	}
}