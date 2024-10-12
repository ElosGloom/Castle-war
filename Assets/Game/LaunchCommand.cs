using FPS;
using MVP;
public class LaunchCommand : SyncCommand
{
    public override void Do()
    {
        new RootPresenter();
        
        
        
        Status = CommandStatus.Success;
    }
}