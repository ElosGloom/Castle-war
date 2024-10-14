using UnityEngine;

namespace FPS
{
    public class LauncherRemoveCommand : SyncCommand
    {
        private readonly AppLauncher _launcher;

        public LauncherRemoveCommand(AppLauncher launcher)
        {
            _launcher = launcher;
        }

        public override void Do()
        {
            Object.Destroy(_launcher);
            Status = CommandStatus.Success;
        }
    }
}