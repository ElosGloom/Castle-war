using ECS.Monobehaviours;
using FPS.Sheets.Converters;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
	public static class UnitsSerializer
	{
		[MenuItem("LevelDesign/Serialize Enemies")]
		public static void Serialize()
		{
			var enemies = Object.FindObjectsByType<UnitView>(FindObjectsSortMode.None);
			var serialized = new UnitDTO[enemies.Length];
			for (var i = 0; i < enemies.Length; i++)
			{
				var enemyUnit = enemies[i];
				var unitTransform = enemyUnit.transform;
				enemyUnit.position = unitTransform.position;
				enemyUnit.rotation = unitTransform.rotation.eulerAngles;
				var unitDTO = new UnitDTO(enemyUnit.position, enemyUnit.rotation, enemyUnit.type);
				serialized[i] = unitDTO;
			}

			var converter = new Vector3Converter();
			var str = JsonConvert.SerializeObject(serialized, converter);
			EditorGUIUtility.systemCopyBuffer = str;
		}
	}
}