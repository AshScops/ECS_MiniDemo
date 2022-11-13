using Unity.Entities;

[GenerateAuthoringComponent]
public struct DeleteComponent : IComponentData
{
    public bool shouldDeleted;
}
