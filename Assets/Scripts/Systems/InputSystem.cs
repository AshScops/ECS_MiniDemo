using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEditor;
using Unity.Mathematics;
using Unity.VisualScripting;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class InputSystem : SystemBase
{

    protected override void OnStartRunning()
    {
        ResetInputComponent();
    }

    protected override void OnUpdate()
    {
        Vector3 direction = Vector3.zero;
        float distance = 0f;
        InputState inputState = InputState.ready;

        //持续按压左键
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            Entities.WithAll<InputComponent>().ForEach((ref InputComponent ic) =>
           {
               ic.x += x;
               ic.y += y;

               ic.inputState = InputState.doing;
           }).Run();

        }

        //更新状态
        Entities.WithAll<InputComponent>().ForEach((ref InputComponent ic) =>
        {
            Vector3 res = new Vector3(ic.x, 0, ic.y);
            direction = res.normalized;

            if (res.magnitude > 30f)
                distance = 30f;
            else
                distance = res.magnitude;

            ic.direction = direction;
            ic.distance = distance;
            inputState = ic.inputState;
        }).Run();

        //松开左键
        if (Input.GetMouseButtonUp(0))
        {
            Entities.WithAll<AddForceComponent>().ForEach((ref AddForceComponent ac) =>
            {
                if (ac.currentState != AddForceState.ready) return;

                ac.direction = direction;
                ac.distance = distance;
                ac.currentState = AddForceState.add;
            }).Run();

            ResetInputComponent();
        }
    }

    public void ResetInputComponent()
    {
        Entities.WithAll<InputComponent>().ForEach((ref InputComponent ic) =>
        {
            ic.x = 0;
            ic.y = 0;
            ic.direction = Vector3.zero;
            ic.distance = 0;
            ic.inputState = InputState.ready;
        }).Run();
    }

}
