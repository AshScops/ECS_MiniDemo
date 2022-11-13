using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct BulletComponent : IComponentData
{
    public float lifetime;
    public float speed;
    public Vector3 direction;
}