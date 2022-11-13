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
        //����ͨJobһ������ʹ�ã��������
        MyCollisionJob collisionJob = new MyCollisionJob()
        {
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>(),
            CharacterComponentGroup = GetComponentDataFromEntity< CharacterComponent>(),
            EnemyComponentGroup = GetComponentDataFromEntity<EnemyComponent>(),
            LocalToWorldGroup = GetComponentDataFromEntity<LocalToWorld>(),
            DeleteComponentGroup = GetComponentDataFromEntity<DeleteComponent>()
        };
        //�����õ�д����
        //Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);

        //��StepPhysicsWorld������ײ�¼���
        Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation, Dependency);
    }
}