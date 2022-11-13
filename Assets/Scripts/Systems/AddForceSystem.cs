using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using static Unity.Physics.Authoring.DebugStream;

public partial class AddForceSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        Entities.WithAll<AddForceComponent>().ForEach((ref AddForceComponent ac) =>
        {
            ac.currentState = AddForceState.ready;
            ac.CD = 0.5f;
        }).Run();
    }

    protected override void OnUpdate()
    {
        //float tmpDeltaTime = Time.DeltaTime;

        Entities.WithAll<AddForceComponent>().ForEach((ref AddForceComponent ac , ref PhysicsVelocity pv) =>
        {
            if (ac.currentState == AddForceState.add)
            {
                pv.Linear = ac.direction * ac.distance;
                ac.currentState = AddForceState.ready;
                //Debug.Log("Linear : Change Done.");
            }
            //else if(ac.currentState == AddForceState.end)
            //{
            //    ac.CD -= tmpDeltaTime;
            //    if(ac.CD <= 0)
            //    {
            //        ac.currentState = AddForceState.ready;
            //        ac.CD = 0.5f;
            //    }
            //}
        }).Run();

    }

}
