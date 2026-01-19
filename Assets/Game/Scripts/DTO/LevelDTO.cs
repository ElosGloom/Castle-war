using System.Collections;
using System.Collections.Generic;
using Converters;
using FPS.Sheets;
using FPS.Sheets.Converters;
using Newtonsoft.Json;

namespace DTO
{
    public readonly struct LevelDTO : ISheetDTO
    {
       
        private readonly string _id;
        public readonly string Prefab;
        public readonly List<UnitDTO> UnitsData;

        public string Id => _id;

        public LevelDTO(IDictionary ht)
        {
            Parser.GetValue(ht[nameof(Id)], out _id);
            Parser.GetValue(ht[nameof(Prefab)], out Prefab);
            Parser.GetValue(ht[nameof(UnitsData)], out UnitsData, new Vector3Converter());
            // var o = ht[nameof(UnitsData)];
            // UnitsData = JsonConvert.DeserializeObject<List<UnitDTO>>(o.ToString(),new Vector3Converter());
        }
    }
}