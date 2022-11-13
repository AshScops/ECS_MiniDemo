using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial class BulletSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime * GameManager.fakeTimeFlowMult;

        Entities.WithAll<BulletComponent>().ForEach((ref BulletComponent bc , ref DeleteComponent dc) =>
        {
            if(bc.lifetime - deltaTime <= 0)
            {
                bc.lifetime = 0;
                dc.shouldDeleted = true;
            }
            else
            {
                bc.lifetime -= deltaTime;
            }

        }).Run();
    }
}
