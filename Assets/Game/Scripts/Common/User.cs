using System;
using DTO;
using Newtonsoft.Json;

namespace Common
{
	public class User : ISerializable
	{
		public readonly Inventory Inventory = new();
		public string Id { get; private set; }
		public int CurrentLevel { get; private set; }

		public string Serialize() => JsonConvert.SerializeObject(this);

		public void SetDefaults(UserDTO dto)
		{
			Id = Guid.NewGuid().ToString();
			CurrentLevel = 1;
			Inventory.Copy(dto.Inventory);
		}

		public void Deserialize(string serializedData)
		{
			var deserializedUser = JsonConvert.DeserializeObject<User>(serializedData);
			Id = deserializedUser.Id;
			CurrentLevel = deserializedUser.CurrentLevel;
			Inventory.Copy(deserializedUser.Inventory);
		}
	}
}