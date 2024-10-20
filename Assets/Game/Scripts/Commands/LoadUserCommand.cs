using Common;
using DTO;
using FPS;
using FPS.Sheets;
using UnityEngine;

namespace Commands
{
	public class LoadUserCommand : SyncCommand
	{
		private readonly DTOStorage _dtoStorage;
		private readonly User _user;

		public LoadUserCommand(DTOStorage dtoStorage, User user)
		{
			_dtoStorage = dtoStorage;
			_user = user;
		}

		public override void Do()
		{
			//local save
			bool hasSave = PlayerPrefs.HasKey(Constants.UserPrefsKey);
			if (hasSave)
			{
				var encodedData = PlayerPrefs.GetString(Constants.UserPrefsKey);
				_user.Deserialize(encodedData);
			}
			else
			{
				_user.SetDefaults(_dtoStorage.GetSingle<UserDTO>());
			}

			Status = CommandStatus.Success;
		}
	}
}