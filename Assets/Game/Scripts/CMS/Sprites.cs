using System;
using FPS;
using UnityEngine;

namespace CMS
{
	[Serializable]
	public struct Sprites
	{
		[SerializeField] private SerializableDictionary<string, Sprite> _container;

		public Sprite this[string id]
		{
			get
			{
				if (_container.TryGetValue(id, out var sprite))
					return sprite;

				Debug.LogError($"Sprite '{id}' not found");
				return null;
			}
		}
	}
}