using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

public partial class MyTriggerEventSystem : SystemBase
{
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    protected override void OnUpdate()
    {
        MyTriggerJob triggerJob = new MyTriggerJob()
        {
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>(),
            CharacterComponentGroup = GetComponentDataFromEntity<CharacterComponent>(),
            EnemyComponentGroup = GetComponentDataFromEntity<EnemyComponent>(),
            BulletComponentGroup = GetComponentDataFromEntity<BulletComponent>(),
            TranslationGroup = GetComponentDataFromEntity<Translation>(),
            DeleteComponentGroup = GetComponentDataFromEntity<DeleteComponent>()
        };

        Dependency = triggerJob.Schedule(stepPhysicsWorld.Simulation, Dependency);
    }
}