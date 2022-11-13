using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(MyCollisionEventSystem))]
[UpdateAfter(typeof(MyTriggerEventSystem))]
[UpdateAfter(typeof(BulletSystem))]
public partial class DeleteSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;

    protected override void OnCreate()
    {
        endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        Entities.WithAll<DeleteComponent>().ForEach((ref DeleteComponent dc) =>
        {
            dc.shouldDeleted = false;

        }).Run();
    }

    protected override void OnUpdate()
    {
        var ecb = endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.WithAll<DeleteComponent>().ForEach((Entity entity, int entityInQueryIndex, in DeleteComponent dc) =>
        {
            if (dc.shouldDeleted)
                ecb.DestroyEntity(entityInQueryIndex, entity);//(entity);

        }).ScheduleParallel();

        endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
