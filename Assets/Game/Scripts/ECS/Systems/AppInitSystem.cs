using Commands;
using Common;
using FPS;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
	public class AppInitSystem : IEcsInitSystem
	{
		public void Init(IEcsSystems systems)
		{
			new GameObject(nameof(RuntimeDispatcher)).AddComponent<RuntimeDispatcher>().Init();

			var queue = new CommandQueue();
			BaseCommandsQueue.Insert(queue);
			
			
			//add other commands
			queue.Enqueue(new ShowUIRootCommand(systems.GetWorld()));
			

			queue.Enqueue(new HideLoaderCommand(queue));
			queue.Execute().Forget();
		}
	}
}