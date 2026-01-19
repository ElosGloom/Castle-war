using System.Collections;
using FPS.Sheets;

namespace DTO
{
	public readonly struct BuildingDTO : ISheetDTO
	{
		public readonly int LoopTime;
		public readonly int Count;
		public readonly int Capacity;
		public readonly string ItemId;
		private readonly string _id;

		public string Id => _id;

		public BuildingDTO(IDictionary ht)
		{
			Parser.GetValue(ht[nameof(Id)], out _id);
			Parser.GetValue(ht[nameof(LoopTime)], out LoopTime);
			Parser.GetValue(ht[nameof(Count)], out Count);
			Parser.GetValue(ht[nameof(Capacity)], out Capacity);
			Parser.GetValue(ht[nameof(ItemId)], out ItemId);
		}
	}
}