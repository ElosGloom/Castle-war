using Common;
using ECS;
using UnityEngine;

namespace FPS
{
    public class AppLauncher : MonoBehaviour
    {
        [SerializeField, Get] private EcsStartup startup;

        private void Awake()
        {
            Launch();
        }

        private void Launch()
        {
            new GameObject(nameof(RuntimeDispatcher)).AddComponent<RuntimeDispatcher>().Init();

            var queue = new CommandQueue();
            BaseCommandsQueue.Insert(queue);


            //add other commands

            queue.Enqueue(new EcsInitCommand(startup));
            queue.Enqueue(new LauncherRemoveCommand(this));
            queue.Enqueue(new HideLoaderCommand(queue));
            queue.Execute().Forget();
        }
    }
}