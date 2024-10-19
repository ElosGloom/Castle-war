using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Newtonsoft.Json;

namespace Converters
{
	public class InventoryConverter : JsonConverter<Inventory>
	{
		public override void WriteJson(JsonWriter writer, Inventory value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("container");

			var dictionary = value.AllItems.ToDictionary(kvp =>
				kvp.Key, kvp => kvp.Value);

			serializer.Serialize(writer, dictionary);

			writer.WriteEndObject();
		}

		public override Inventory ReadJson(JsonReader reader, Type objectType, Inventory existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			var inventory = new Inventory();

			reader.Read();
			while (reader.TokenType != JsonToken.EndObject)
			{
				string propertyName = reader.Value!.ToString();
				reader.Read();

				if (propertyName == "container")
				{
					var container = serializer.Deserialize<Dictionary<string, int>>(reader);
					inventory.Copy(container);
				}

				reader.Read();
			}

			return inventory;
		}
	}
}