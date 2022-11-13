using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct TimeComponent : IComponentData
{
    public TimeFlowState timeFlowState;
}

public enum TimeFlowState
{ 
    normal = 0,
    slow
}
