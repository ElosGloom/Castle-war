using System.Collections;
using FPS.Sheets;

namespace DTO
{
	public class CategoryDTO : ISheetDTO
	{
		public string Id => string.Empty;
		public readonly string[] Currency;
		public readonly string[] Army;
		public readonly string[] Buildings;

		public CategoryDTO(IDictionary ht)
		{
			Parser.GetValue(ht[nameof(Currency)], out Currency);
			Parser.GetValue(ht[nameof(Army)], out Army);
			Parser.GetValue(ht[nameof(Buildings)], out Buildings);
		}
	}
}