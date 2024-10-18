using System;
using Converters;
using DTO;
using Newtonsoft.Json;

namespace Common
{
	public class User : ISerializable
	{
		[JsonProperty] public readonly Inventory Inventory = new();
		[JsonProperty] public string Id { get; private set; }
		[JsonProperty] public int CurrentLevel { get; private set; }

		public string Serialize()
		{
			var serializeObject = JsonConvert.SerializeObject(this, new InventoryConverter());
			return serializeObject;
		}

		public void SetDefaults(UserDTO dto)
		{
			Id = Guid.NewGuid().ToString();
			CurrentLevel = 1;
			Inventory.Copy(dto.Inventory);
		}

		public void Deserialize(string serializedData)
		{
			var deserializedUser = JsonConvert.DeserializeObject<User>(serializedData, new InventoryConverter());
			Id = deserializedUser.Id;
			CurrentLevel = deserializedUser.CurrentLevel;
			Inventory.Copy(deserializedUser.Inventory);
		}
	}
}