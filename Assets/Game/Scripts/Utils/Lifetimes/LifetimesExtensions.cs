using JetBrains.Lifetimes;
using UnityEngine;

namespace Utils
{
	public static class LifetimesExtensions
	{
		public static Lifetime GetLifetime(this GameObject go) =>
			go.TryGetComponent<LifetimeMono>(out var mono) ? mono.Lifetime : go.AddComponent<LifetimeMono>().Lifetime;
	}
}