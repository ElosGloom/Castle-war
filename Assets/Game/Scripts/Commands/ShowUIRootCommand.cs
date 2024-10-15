using System;
using ECS;
using FPS;
using Leopotam.EcsLite;
using UI;
using UnityEngine;

namespace Commands
{
	public class ShowUIRootCommand : SyncCommand
	{
		private readonly EcsWorld _world;

		public ShowUIRootCommand(EcsWorld world)
		{
			_world = world;
		}

		public override void Do()
		{
			try
			{
				UIHelper.ShowWindow<UIMainMenuWindow>(_world);
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