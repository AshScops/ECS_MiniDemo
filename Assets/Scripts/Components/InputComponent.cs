using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]//添加此注解使该其能够被挂载至Entity上
public struct InputComponent : IComponentData
{
    public float x;
    public float y;
    public float distance;
    public Vector3 direction;
    public InputState inputState;
}

public enum InputState
{
    ready = 0,
    doing
}