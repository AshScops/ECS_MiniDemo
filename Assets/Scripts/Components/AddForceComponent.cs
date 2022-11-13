using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]//��Ӵ�ע��ʹ�����ܹ���������Entity��
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