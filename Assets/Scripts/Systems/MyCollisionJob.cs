using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

struct MyCollisionJob : ICollisionEventsJob
{
    public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;
    public ComponentDataFromEntity<CharacterComponent> CharacterComponentGroup;
    public ComponentDataFromEntity<EnemyComponent> EnemyComponentGroup;
    public ComponentDataFromEntity<LocalToWorld> LocalToWorldGroup;
    public ComponentDataFromEntity<DeleteComponent> DeleteComponentGroup;

    public void Execute(CollisionEvent collisionEvent)
    {
        if (EnemyComponentGroup.HasComponent(collisionEvent.EntityA))
        {
            Debug.Log("通知删除命令队列A");
            //DeleteComponent dc = DeleteComponentGroup[collisionEvent.EntityA];
            //dc.shouldDeleted = true;
            //DeleteComponentGroup[collisionEvent.EntityA] = dc;
        }
        else if (EnemyComponentGroup.HasComponent(collisionEvent.EntityB))
        {
            Debug.Log("通知删除命令队列B");

            DeleteComponent dc = DeleteComponentGroup[collisionEvent.EntityB];
            dc.shouldDeleted = true;
            DeleteComponentGroup[collisionEvent.EntityB] = dc;

            PhysicsVelocity pv = PhysicsVelocityGroup[collisionEvent.EntityA];
            pv.Linear *= -1f;
            PhysicsVelocityGroup[collisionEvent.EntityA] = pv;

            CharacterComponent cc = CharacterComponentGroup[collisionEvent.EntityA];
            cc.score++;
            CharacterComponentGroup[collisionEvent.EntityA] = cc;

            //TODO:加特效

        }
    }

}

