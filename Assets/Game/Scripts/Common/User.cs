using System;
using System.Collections.Generic;
using Converters;
using DTO;
using FPS;
using Newtonsoft.Json;
using Utils;

namespace Common
{
	public class User : ISerializable
	{
		public float Playtime;
		[JsonProperty] public readonly Inventory Inventory = new();
		[JsonProperty] public Dictionary<string, TimeSpan> Progress = new();
		[JsonProperty] public string Id { get; private set; }
		[JsonProperty] public int CurrentLevel { get; private set; }

		public string Serialize() => JsonConvert.SerializeObject(this, new InventoryConverter());

		public void SetDefaults(UserDTO dto)
		{
			Id = Guid.NewGuid().ToString();
			CurrentLevel = 1;
			
			Inventory.Copy(dto.Inventory);
		}

		public void Deserialize(string decodedJson)
		{
			var deserializedUser = JsonConvert.DeserializeObject<User>(decodedJson, new InventoryConverter());
			Id = deserializedUser.Id;
			CurrentLevel = deserializedUser.CurrentLevel;
			Playtime = deserializedUser.Playtime;
			Inventory.Copy(deserializedUser.Inventory);
			deserializedUser.Progress.ReplaceAll(Progress);
		}
	}
}