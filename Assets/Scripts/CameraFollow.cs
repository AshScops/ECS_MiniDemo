using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;//����������ҵ�λ��

    private Vector3 pos;
    public float speed;

    private EntityManager manager;
    private float3 tempPos;
    public Entity targetEntity;
    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //����һ����ѯ ����ѯʵ��������Character�����Translation���   
        var queryDescription = new EntityQueryDesc
        {
            All = new ComponentType[] { ComponentType.ReadOnly<CharacterComponent>(), ComponentType.ReadOnly<Translation>() }
        };

        EntityQuery players = manager.CreateEntityQuery(queryDescription);
        //������ֻ��������Character���������ֱ�ӻ�ȡ����
        if (players.CalculateEntityCount() != 0)
        {
            NativeArray<Entity> temp = new NativeArray<Entity>(1, Allocator.Temp);
            temp = players.ToEntityArray(Allocator.Temp);
            targetEntity = temp[0];
            temp.Dispose();
        }
        players.Dispose();

    }

    void Update()
    {
        if (targetEntity != Entity.Null)
        {
            if (manager.HasComponent<Translation>(targetEntity))
            {
                tempPos = manager.GetComponentData<Translation>(targetEntity).Value;
            }
        }

        transform.position = Vector3.Lerp(transform.position, (Vector3)tempPos + offset, speed * Time.deltaTime);//������������֮��ľ���
    }
}
