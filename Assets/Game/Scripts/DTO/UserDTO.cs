using System.Collections;
using System.Collections.Generic;
using FPS.Sheets;

namespace DTO
{
	public readonly struct UserDTO : ISheetDTO
	{
		public readonly Dictionary<string, int> Inventory;

		public string Id => string.Empty;

		public UserDTO(IDictionary ht)
		{
			Parser.GetValue(ht[nameof(Inventory)], out Inventory);
		}
	}
}