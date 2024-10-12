using UnityEngine;

namespace FPS
{
    public class AppLauncher : MonoBehaviour
    {
        private void Start()
        {
            Launch();
        }

        private void Launch()
        {
            new GameObject(nameof(RuntimeDispatcher)).AddComponent<RuntimeDispatcher>().Init();

            var queue = new CommandQueue();
            BaseCommandsQueue.Insert(queue);


            //add other commands


            queue.Enqueue(new HideLoaderCommand(queue));
            queue.Execute().Forget();
        }
    }
}