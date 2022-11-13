using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]//��Ӵ�ע��ʹ�����ܹ���������Entity��
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