using Common;
using DTO;
using FPS;
using FPS.Sheets;

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
			bool hasSave = false;
			if (hasSave)
			{
				_user.Load();
			}
			else
			{
				var dto = _dtoStorage.GetSingle<UserDTO>();
				foreach (var kvp in dto.Inventory)
				{
					_user.Inventory[kvp.Key] = kvp.Value;
				}
			}
		}
	}
}