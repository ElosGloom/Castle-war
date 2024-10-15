using System;
using System.Collections.Generic;
using FPS;
using FPS.Pool;
using Game.Scripts.ECS.Monobehaviours;

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
}