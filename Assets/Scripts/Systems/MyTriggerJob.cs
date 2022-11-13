using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

struct MyTriggerJob : ITriggerEventsJob
{
    public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;
    public ComponentDataFromEntity<CharacterComponent> CharacterComponentGroup;
    public ComponentDataFromEntity<EnemyComponent> EnemyComponentGroup;
    public ComponentDataFromEntity<BulletComponent> BulletComponentGroup;
    public ComponentDataFromEntity<Translation> TranslationGroup;
    public ComponentDataFromEntity<DeleteComponent> DeleteComponentGroup;

    public void Execute(TriggerEvent triggerEvent)
    {
        if (BulletComponentGroup.HasComponent(triggerEvent.EntityA))
        {
            if (!CharacterComponentGroup.HasComponent(triggerEvent.EntityB)) return;
            Debug.Log("TriggerDone!A");

            DeleteComponent dc = DeleteComponentGroup[triggerEvent.EntityA];
            if (dc.shouldDeleted) return;
            dc.shouldDeleted = true;
            DeleteComponentGroup[triggerEvent.EntityA] = dc;
            
            CharacterComponent cc = CharacterComponentGroup[triggerEvent.EntityB];
            if (cc.health - 1 < 0) cc.health = 0;
            else cc.health--;
            CharacterComponentGroup[triggerEvent.EntityB] = cc;
        }
        else if (BulletComponentGroup.HasComponent(triggerEvent.EntityB))
        {
            if( ! CharacterComponentGroup.HasComponent(triggerEvent.EntityA)) return;
            Debug.Log("TriggerDone!B");

            DeleteComponent dc = DeleteComponentGroup[triggerEvent.EntityB];
            if (dc.shouldDeleted) return;
            dc.shouldDeleted = true;
            DeleteComponentGroup[triggerEvent.EntityB] = dc;

            CharacterComponent cc = CharacterComponentGroup[triggerEvent.EntityA];
            if (cc.health - 1 < 0) cc.health = 0;
            else cc.health--;
            CharacterComponentGroup[triggerEvent.EntityA] = cc;

            //TODO:¼ÓÌØÐ§

        }
    }
}

