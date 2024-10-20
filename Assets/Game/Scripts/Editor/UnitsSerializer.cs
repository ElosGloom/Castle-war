using System.Collections.Generic;
using Converters;
using ECS.Monobehaviours;
using FPS.Sheets.Converters;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
    public static class UnitsSerializer
    {
        private static readonly List<UnitDTO> _unitsList = new();

        [MenuItem("LevelDesign/Safe and Serialize Units")]
        public static void SafeUnits()
        {
            UnitView[] allComponents = Object.FindObjectsByType<UnitView>(FindObjectsSortMode.None);
            foreach (var enemyUnit in allComponents)
            {
                var unitTransform = enemyUnit.transform;
                enemyUnit.position = unitTransform.position;
                enemyUnit.rotation = unitTransform.rotation.eulerAngles;
                var unitDTO = new UnitDTO(enemyUnit.position,enemyUnit.rotation,enemyUnit.type);
                _unitsList.Add(unitDTO);
            }
            
            var converter = new Vector3Converter();
            var str =JsonConvert.SerializeObject(_unitsList,converter);
            EditorGUIUtility.systemCopyBuffer = str;
            _unitsList.Clear();
            
        }

    }
}