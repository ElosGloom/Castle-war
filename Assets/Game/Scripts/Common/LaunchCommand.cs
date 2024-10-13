using FPS;
using MVP;

namespace Common
{
    public class LaunchCommand : SyncCommand
    {
        public override void Do()
        {
            RootPresenter.Create();
            Status = CommandStatus.Success;
        }
    }
}