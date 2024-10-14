using ECS;
using FPS;

namespace Common
{
    public class EcsInitCommand : SyncCommand
    {
        private readonly EcsStartup _startup;

        public EcsInitCommand(EcsStartup startup)
        {
            _startup = startup;
        }

        public override void Do()
        {
            _startup.Init();
            Status = CommandStatus.Success;
        }
    }
}