using System;
using Converters;
using DTO;
using FPS;
using Newtonsoft.Json;

namespace Common
{
	public class User : ISerializable
	{
		public readonly Inventory Inventory = new();
		public float Playtime;
		[JsonProperty] public string Id { get; private set; }
		[JsonProperty] public int CurrentLevel { get; private set; }

		public string Serialize()
		{
			var raw = JsonConvert.SerializeObject(this, new InventoryConverter());
			return GZip.Encode(raw);
		}

		public void SetDefaults(UserDTO dto)
		{
			Id = Guid.NewGuid().ToString();
			CurrentLevel = 1;
			Inventory.Copy(dto.Inventory);
		}

		public void Deserialize(string encoded)
		{
			var decoded = GZip.Decode(encoded);
			var deserializedUser = JsonConvert.DeserializeObject<User>(decoded, new InventoryConverter());
			Id = deserializedUser.Id;
			CurrentLevel = deserializedUser.CurrentLevel;
			Playtime = deserializedUser.Playtime;
			Inventory.Copy(deserializedUser.Inventory);
		}
	}
}