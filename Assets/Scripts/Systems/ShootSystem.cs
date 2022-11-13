using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.Transforms;
using UnityEngine;
using static UnityEditor.Progress;

[UpdateAfter(typeof(MyCollisionEventSystem))]
public partial class ShootSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;

    protected override void OnCreate()
    {
        endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    struct MyFliter
    {
        public Translation translation;
        public Entity entity;
    };

    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = endSimulationEcbSystem.CreateCommandBuffer();
        float deltaTime = Time.DeltaTime * GameManager.fakeTimeFlowMult;
        Translation targetPos = new Translation();
        //NativeArray<Translation> selfPos = new NativeArray<Translation>(GameManager.enemyMaxCount, Allocator.TempJob);
        //NativeArray<Entity> entity = new NativeArray<Entity>(GameManager.enemyMaxCount, Allocator.TempJob);
        NativeList<MyFliter> myFliters = new NativeList<MyFliter>(Allocator.TempJob);
        int index = 0;

        Entities.WithAll<ShootComponent, EnemyComponent>().ForEach((ref Entity en, ref Translation t, ref ShootComponent sc) =>
        {
            if (sc.shootCD == 0)
            {
                //sc.shootCD = 2.7f;

                //selfPos[index] = t;
                //entity[index] = en;
                //myFliters[index] = new MyFliter
                //{
                //    translation = t,
                //    entity = en
                //};

                myFliters.Add(new MyFliter
                {
                    translation = t,
                    entity = en
                });
                index++;
            }
            else
            {
                if (sc.shootCD - deltaTime <= 0)
                    sc.shootCD = 0;
                else
                    sc.shootCD -= deltaTime;
            }

        }).Run();

        //if(index != 0)
        //{
        //    foreach(var e in entity)
        //    {
        //        Debug.Log(e.Index);
        //    }
        //}


        Entities.WithAll<CharacterComponent>().ForEach((in Translation t) =>
        {
            targetPos = t;
        }).Run();

        Entity template = GameManager.instance.bulletEntity;

        //foreach (var s in selfPos)
        foreach (var mf in myFliters)
        {
            //Debug.Log("mf DOING");
            //if (mf.entity.Index == 0) continue;

            Entity temp = ecb.Instantiate(template);
            Vector3 direction = ((Vector3)targetPos.Value - (Vector3)mf.translation.Value).normalized;

            Translation translation = new Translation
            {
                Value = mf.translation.Value + new float3(direction)
            };

            BulletComponent bulletComponent = new BulletComponent
            {
                lifetime = 5f,
                speed = 5f,
                direction = direction,
            };

            PhysicsVelocity physicsVelocity = new PhysicsVelocity
            {
                Linear = direction * 10f * GameManager.fakeTimeFlowMult
            };

            ecb.SetName(temp, "Bullet");
            ecb.SetComponent(temp, translation);
            ecb.SetComponent(temp, bulletComponent);
            ecb.SetComponent(temp, physicsVelocity);
            //Debug.Log("Done!");
        }

        //entity.Dispose();
        //selfPos.Dispose();
        myFliters.Dispose();
    }
}
