namespace ECS.FSM
{
	public interface IState
	{
		void Enter();
		void Update();
		void Exit();
	}
}