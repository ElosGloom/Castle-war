namespace ECS.FSM
{
	public interface IStateHandler
	{
		AppState TargetState { get; }
	}
}