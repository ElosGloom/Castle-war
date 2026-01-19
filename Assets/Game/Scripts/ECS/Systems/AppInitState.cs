using Commands;
using ECS.FSM;
using FPS;
using Leopotam.EcsLite;
using VContainer;

namespace ECS.Systems
{
	public class AppInitState : IEcsSystem, IStateEnter
	{
		private readonly IObjectResolver _resolver;
		
		public AppState TargetState => AppState.Init;


		[Inject]
		public AppInitState(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public void Enter()
		{
			var queue = _resolver.Resolve<CommandQueue>();
			_resolver.Resolve<BaseInitializationCommands>().Insert(queue);

			
			queue.Enqueue(_resolver.Resolve<LoadLocalDataCommand>());
			queue.Enqueue(_resolver.Resolve<LoginCommand>());


			queue.Enqueue(_resolver.Resolve<HideLoaderCommand>().WithParams(queue));
			queue.Execute().Forget();
		}
	}
}