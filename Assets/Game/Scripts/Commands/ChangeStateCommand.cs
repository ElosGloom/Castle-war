using System;
using ECS.FSM;
using FPS;
using UnityEngine;

namespace Commands
{
	public class ChangeStateCommand<T> : SyncCommand where T : IState
	{
		public override string Name => $"ChangeStateCommand:{typeof(T).Name}";

		public override void Do()
		{
			try
			{
				AppStateMachine.SetState<T>();
				Status = CommandStatus.Success;
			}
			catch (Exception e)
			{
				Status = CommandStatus.Error;
				Debug.LogException(e);
			}
		}
	}
}