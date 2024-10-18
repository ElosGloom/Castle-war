using System;
using System.Collections.Generic;
using ECS.Systems;
using Leopotam.EcsLite;

namespace ECS.FSM
{
	public class AppStateMachine : IEcsRunSystem, IEcsInitSystem
	{
		private static EcsWorld _world;
		private static int _currentStateEntity;
		private static IState _currentState;

		private static readonly Dictionary<Type, IState> States = new();


		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			foreach (var system in systems.GetAllSystems())
			{
				if (system is IState state)
				{
					States.Add(system.GetType(), state);
				}
			}

			SetState<AppInitState>();
		}

		public void Run(IEcsSystems systems)
		{
			_currentState.Update();
		}

		public static void SetState<T>() where T : IState
		{
			if (_currentState != null)
			{
				_currentState.Exit();
				_world.DelEntity(_currentStateEntity);
			}

			var stateEntity = _world.NewEntity();
			var pool = _world.GetPool<StateComponent<T>>();
			pool.Add(stateEntity);
			_currentState = States[typeof(T)];
			_currentState.Enter();
		}
	}
}