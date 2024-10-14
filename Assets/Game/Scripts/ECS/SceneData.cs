using UnityEngine;

namespace Game.Scripts.ECS
{
    public class SceneData: MonoBehaviour

    {
        [field: SerializeField] public Camera Camera { get; private set; }

    }
}