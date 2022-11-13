using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

public partial class MyCollisionEventSystem : SystemBase
{
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate()
    {
        //像普通Job一样进行使用，添加依赖
        MyCollisionJob collisionJob = new MyCollisionJob()
        {
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>(),
            CharacterComponentGroup = GetComponentDataFromEntity< CharacterComponent>(),
            EnemyComponentGroup = GetComponentDataFromEntity<EnemyComponent>(),
            LocalToWorldGroup = GetComponentDataFromEntity<LocalToWorld>(),
            DeleteComponentGroup = GetComponentDataFromEntity<DeleteComponent>()
        };
        //已弃用的写法：
        //Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);

        //由StepPhysicsWorld接收碰撞事件。
        Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation, Dependency);
    }
}