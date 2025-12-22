using System.Collections;
using System.Collections.Generic;
using FPS.Sheets;

namespace DTO
{
	public class CategoryDTO : ISheetDTO
	{
		public string Id => string.Empty;
		public readonly HashSet<string> Currency;
		public readonly HashSet<string> Army;
		public readonly HashSet<string> Buildings;

		public CategoryDTO(IDictionary ht)
		{
			Parser.GetValue(ht[nameof(Currency)], out Currency);
			Parser.GetValue(ht[nameof(Army)], out Army);
			Parser.GetValue(ht[nameof(Buildings)], out Buildings);
		}
	}
}