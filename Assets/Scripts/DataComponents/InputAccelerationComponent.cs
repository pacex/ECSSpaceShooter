using Unity.Entities;

[GenerateAuthoringComponent]
public struct InputAccelerationComponent : IComponentData
{
    //Player Input acceleration
    public float Linear;
    public float Angular;
}