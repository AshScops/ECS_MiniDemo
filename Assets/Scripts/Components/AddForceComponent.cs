using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]//添加此注解使该其能够被挂载至Entity上
public struct AddForceComponent : IComponentData
{
    public AddForceState currentState;
    public float distance;
    public Vector3 direction;
    public float CD;
}

public enum AddForceState
{
    ready = 0,
    add,
    end
}