using System;
using ECS.Monobehaviours;
using FPS.UI;
using UnityEngine;

namespace ECS
{
	public struct MonoView<T> where T : MonoBehaviour
	{
		public T View;
	}
	
	public struct UnitComponent //todo: MonoReference
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
	public struct ClickRequest { }
	public struct CreateRequest { }
	public struct CleanRequest { }

	public struct UnitSpawnRequest
	{
		public Vector3 Position;
	}

	public struct TimerComponent
	{
		public Action Callback;
		public float LoopTime;
		public float TimeLeft;
		
		public bool Loop => LoopTime > 0;
	}
}