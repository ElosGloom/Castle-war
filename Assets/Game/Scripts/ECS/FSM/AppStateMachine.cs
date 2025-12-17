using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace ECS.FSM
{
	public interface IAppStateMachine : IEcsSystem
	{
		void SetState(AppState targetState);
	}

	public class AppStateMachine : IEcsRunSystem, IEcsInitSystem, IAppStateMachine
	{
		private AppState _currentState;
		private readonly Dictionary<AppState, StateHandlers> _states = new();


		public void Init(IEcsSystems systems)
		{
			Dictionary<AppState, HandlersBuilder> builders = new();
			foreach (var system in systems.GetAllSystems())
			{
				if (system is not IStateHandler state)
					continue;

				builders.TryAdd(state.TargetState, new());
				builders[state.TargetState].Add(state);
			}

			foreach (var kvp in builders)
			{
				_states.TryAdd(kvp.Key, new(kvp.Value));
			}

			_currentState = AppState.Init;
			_states[_currentState].Enter();
		}

		public void Run(IEcsSystems systems)
		{
			_states[_currentState].Update();
		}

		public void SetState(AppState targetState)
		{
			_states[_currentState].Exit();
			_currentState = targetState;
			_states[_currentState].Enter();
		}

		private class HandlersBuilder
		{
			private HashSet<IStateEnter> _enterHandlers = new();
			private HashSet<IStateUpdate> _updateHandlers = new();
			private HashSet<IStateExit> _exitHandlers = new();

			public void Build(
				out IStateEnter[] enterHandlers,
				out IStateUpdate[] updateHandlers,
				out IStateExit[] exitHandlers)
			{
				enterHandlers = _enterHandlers.ToArray();
				updateHandlers = _updateHandlers.ToArray();
				exitHandlers = _exitHandlers.ToArray();

				_enterHandlers = null;
				_updateHandlers = null;
				_exitHandlers = null;
			}

			public void Add(IStateHandler state)
			{
				if (state is IStateEnter enterHandle)
					_enterHandlers.Add(enterHandle);

				if (state is IStateUpdate updateHandle)
					_updateHandlers.Add(updateHandle);

				if (state is IStateExit exitHandle)
					_exitHandlers.Add(exitHandle);
			}
		}

		private class StateHandlers
		{
			private readonly IStateEnter[] _enterHandlers;
			private readonly IStateUpdate[] _updateHandlers;
			private readonly IStateExit[] _exitHandlers;

			public StateHandlers(HandlersBuilder builder)
			{
				builder.Build(out _enterHandlers, out _updateHandlers, out _exitHandlers);
			}

			public void Update()
			{
				foreach (var handle in _updateHandlers)
					handle.Update();
			}

			public void Enter()
			{
				foreach (var handle in _enterHandlers)
					handle.Enter();
			}

			public void Exit()
			{
				foreach (var handle in _exitHandlers)
					handle.Exit();
			}
		}
	}
}