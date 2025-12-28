using System;
using Buildings;
using FPS;
using UnityEngine;

namespace CMS
{
	[Serializable]
	public struct Prefabs
	{
		[SerializeField] private SerializableDictionary<string, BuildingView> _buildings;

		public BuildingView GetBuilding(string name) => _buildings[name];
	}
}