using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;


[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateAfter(typeof(InputSystem))]
public partial class DrawSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float distance = 0;
        Vector3 direction = Vector3.zero;
        Vector3 pos = Vector3.zero;

        Entities.WithAll<InputComponent>().ForEach((ref InputComponent ic) =>
        {
            if(ic.x != 0 || ic.y != 0)
            {
                direction = ic.direction;
                distance = ic.distance;
            }

        }).Run();

        if (distance != 0)
        {
            Entities.WithAll<CharacterComponent , LocalToWorld>().ForEach((ref LocalToWorld ltw) =>
            {
                pos = ltw.Position;

            }).Run();

            Debug.DrawLine(pos, pos + direction * distance / 5f);
        }
    }
}
