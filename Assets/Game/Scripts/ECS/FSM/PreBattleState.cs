using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.FSM
{
	public class PreBattleState : IEcsSystem, IState
	{
		public void Enter()
		{
			
		}

		public void Update()
		{
			Debug.Log(123);
		}

		public void Exit()
		{
		}
	}
}