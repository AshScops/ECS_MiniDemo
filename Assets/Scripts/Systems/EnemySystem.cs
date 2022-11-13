using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using System;
using UnityEngine;

public partial class EnemySystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;
    //����ɸѡ�����ĵ��˵Ķ���
    private EntityQuery query;

    private uint seed = (uint)DateTime.Now.Second;

    protected override void OnCreate()
    {
        endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        if (seed == 0 || seed > 10e8) seed = 66;
        Unity.Mathematics.Random random = new Unity.Mathematics.Random(seed);
        seed += 13;

        float deltaTime = Time.DeltaTime;

        EntityCommandBuffer ecb = endSimulationEcbSystem.CreateCommandBuffer();

        Entities.
            WithStoreEntityQueryInField(ref query).
            ForEach((Entity entity, ref Translation translation, ref Rotation rotation, ref EnemyComponent ec) =>
            {
                //if (HasComponent<LocalToWorld>(ec.targetEntity))
                //{
                    //׷������
                    //LocalToWorld targetl2w = GetComponent<LocalToWorld>(sc.targetEntity);
                    //float3 targetPos = targetl2w.Position;
                    //translation.Value = Vector3.MoveTowards(translation.Value, targetPos, sc.speed * deltaTime);

                    //var targetDir = targetPos - translation.Value;
                    //quaternion temp1 = quaternion.LookRotation(targetDir, math.up());
                    //rotation.Value = temp1;
                //}
            }).Run();

        if (query.CalculateEntityCount() < GameManager.enemyMaxCount)
        {
            Entity characterEntity = GetSingletonEntity<CharacterComponent>();

            for (int i = query.CalculateEntityCount(); i < GameManager.enemyMaxCount; i++)
            {
                //Entity temp = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                //    GameManager.instance.enemyPrefab, GameManager.instance._settings);
                //Entity template = GameManager.instance.enemyEntity;
                Entity temp = ecb.Instantiate(GameManager.instance.enemyEntity);

                //#region ���λ�����ɵ���
                //x:-18_+18
                //y:-8_+17
                float x = random.NextFloat(-18f, 18f), y = 2f, z = random.NextFloat(-8f, 17f);
                //#endregion

                Translation translation = new Translation
                {
                    Value = new float3(x, y, z)
                };
                //Ԥ��������������ݣ�����Ϊ��Ҫ���¸�ֵ
                //������Ϊ�����е�����Ԥ����Ҫ�ڳ������к����ת��ΪEntity������ת��ʱ�䲻ȷ�������Եȴ������ɺ����¸�ֵ
                //ShootComponent sc = new ShootComponent
                //{
                //    shootCD = 3f + random.NextFloat(-1f, 1f),
                //    targetEntity = characterEntity
                //};

                ecb.SetName(temp, "Enemy");
                ecb.SetComponent(temp, translation);
                //ecb.SetComponent(temp, sc);
            }
        }
    }
}
