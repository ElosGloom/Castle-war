using Commands;
using Common;
using ECS.FSM;
using FPS;
using FPS.Sheets;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
	public class AppInitState : IEcsSystem, IState
	{
		private readonly EcsWorldInject _world;
		private readonly EcsCustomInject<DTOStorage> _dtoStorage;
		private readonly EcsCustomInject<User> _user;

		public void Enter()
		{
			new GameObject(nameof(RuntimeDispatcher)).AddComponent<RuntimeDispatcher>().Init();

			var queue = new CommandQueue();
			BaseCommands.Insert(queue);
			SheetCommands.Insert(queue, _dtoStorage.Value);


			//add other commands
			queue.Enqueue(new LoadUserCommand(_dtoStorage.Value, _user.Value));


			queue.Enqueue(new HideLoaderCommand(queue));
			queue.Enqueue(new ChangeStateCommand<MainMenuState>());
			queue.Execute().Forget();
		}

		public void Update() { }

		public void Exit() { }
	}
}