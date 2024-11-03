using Commands;
using Common;
using ECS.FSM;
using FPS;
using FPS.Sheets;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Network;
using UnityEngine;

namespace ECS.Systems
{
	public class AppInitState : IEcsSystem, IState
	{
		private readonly EcsWorldInject _world;
		private readonly EcsCustomInject<DTOStorage> _dtoStorage;
		private readonly EcsCustomInject<User> _user;
		private readonly EcsCustomInject<ApiService> _apiService;

		public void Enter()
		{
			new GameObject(nameof(RuntimeDispatcher)).AddComponent<RuntimeDispatcher>().Init();

			var queue = new CommandQueue();
			BaseCommands.Insert(queue);
			SheetCommands.Insert(queue, _dtoStorage.Value);


			//add other commands
			queue.Enqueue(new LoadLocalDataCommand(_dtoStorage.Value, _user.Value));
			queue.Enqueue(new LoginCommand(_apiService.Value, _world.Value, _user.Value));


			queue.Enqueue(new HideLoaderCommand(queue));
			queue.Execute().Forget();
		}

		public void Update() { }

		public void Exit() { }
	}
}