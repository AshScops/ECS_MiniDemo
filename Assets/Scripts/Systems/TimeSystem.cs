using Unity.Entities;
using Unity.Physics;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateAfter(typeof(InputSystem))]
public partial class TimeSystem : SystemBase
{
    protected override void OnUpdate()
    {
        InputState inputState = InputState.ready;
        TimeFlowState timeFlowState = TimeFlowState.normal;

        Entities.WithAll<InputComponent, TimeComponent>().ForEach((ref InputComponent ic, ref TimeComponent tc) =>
        {
            inputState = ic.inputState;
            timeFlowState = tc.timeFlowState;
        }).Run();

        if(inputState == InputState.doing && timeFlowState == TimeFlowState.normal)
        {
            Entities.WithAll<PhysicsVelocity>().ForEach((ref PhysicsVelocity pv) =>
            {
                pv.Linear *= 0.1f;
            }).Run();

            Entities.WithAll<TimeComponent>().ForEach((ref TimeComponent tc) =>
            {
                tc.timeFlowState = TimeFlowState.slow;
            }).Run();

            GameManager.fakeTimeFlowMult = 0.1f;
        }
        else if(inputState == InputState.ready && timeFlowState == TimeFlowState.slow)
        {
            Entities.WithAll<PhysicsVelocity>().ForEach((ref PhysicsVelocity pv) =>
            {
                pv.Linear *= 10f;
            }).Run();

            Entities.WithAll<TimeComponent>().ForEach((ref TimeComponent tc) =>
            {
                tc.timeFlowState = TimeFlowState.normal;
            }).Run();

            GameManager.fakeTimeFlowMult = 1f;
        }

    }
}
