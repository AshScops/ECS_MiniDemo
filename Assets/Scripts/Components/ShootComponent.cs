using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct ShootComponent : IComponentData
{
    public float shootCD;
    public Entity targetEntity;
}
