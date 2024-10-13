using FPS;
using MVP;
public class LaunchCommand : SyncCommand
{
    public override void Do()
    {
        RootPresenter.Create();
        Status = CommandStatus.Success;
    }
}