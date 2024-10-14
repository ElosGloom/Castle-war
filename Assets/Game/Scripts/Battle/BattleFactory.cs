using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle
{
    public static class BattleFactory
    {
        public static GameObject SetupScene(int lvl)
        {
            return Object.Instantiate(Resources.Load<GameObject>($"Lvl{lvl}"));
        }
    }
}
