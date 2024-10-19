using UnityEngine;

public class UnitDTO
{
    public Vector3 Position { get; }
    public Vector3 Rotation { get; }
    public string Type { get; }


    public UnitDTO(Vector3 position, Vector3 rotation, string type)
    {
        Position = position;
        Rotation = rotation;
        Type = type;
    }
}