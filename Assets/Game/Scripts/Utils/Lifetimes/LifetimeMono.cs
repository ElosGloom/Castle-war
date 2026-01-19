using JetBrains.Lifetimes;
using UnityEngine;

namespace Utils
{
	public class LifetimeMono : MonoBehaviour
	{
		private readonly LifetimeDefinition _lifetimeDefinition = new();
		public Lifetime Lifetime => _lifetimeDefinition.Lifetime;

		private void OnDestroy()
		{
			_lifetimeDefinition.Terminate();
		}
	}
}