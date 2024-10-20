using Newtonsoft.Json;
using UnityEngine;

public readonly struct UnitDTO
{
    public readonly Vector3 Position;
    public readonly Vector3 Rotation;
    public readonly string Type;


    [JsonConstructor]
    public UnitDTO(Vector3 position, Vector3 rotation, string type)
    {
        Position = position;
        Rotation = rotation;
        Type = type;
    }
}