using System.Collections;
using FPS.Sheets;

namespace DTO
{
	public readonly struct UnitStatsDTO : ISheetDTO
	{
		public readonly float Health;
		public readonly float Armor;
		public readonly float Range;
		public readonly float Attack;
		private readonly string _id;

		public string Id => _id;

		public UnitStatsDTO(IDictionary ht)
		{
			Parser.GetValue(ht[nameof(Id)], out _id);
			Parser.GetValue(ht[nameof(Health)], out Health);
			Parser.GetValue(ht[nameof(Armor)], out Armor);
			Parser.GetValue(ht[nameof(Range)], out Range);
			Parser.GetValue(ht[nameof(Attack)], out Attack);
		}
	}
}