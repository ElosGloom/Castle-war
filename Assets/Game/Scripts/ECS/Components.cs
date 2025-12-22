using System;
using ECS.Monobehaviours;
using FPS.UI;
using UnityEngine;

namespace ECS
{
	public struct UnitComponent
	{
		public UnitView UnitView;
	}

	// public struct PoolablesHolder
	// {
	// 	public readonly HashSet<Poolable> Poolables;
	//
	// 	public PoolablesHolder()
	// 	{
	// 		Poolables = new();
	// 	}
	// }

	public struct WindowComponent
	{
		public Type WindowType;
		public Action WindowCloseCallback;
	}

	public struct OpenWindowRequest<T> where T : IWindow { }

	public struct CloseWindowRequest { }

	public struct UnitSpawnRequest
	{
		public Vector3 Position;
	}
}