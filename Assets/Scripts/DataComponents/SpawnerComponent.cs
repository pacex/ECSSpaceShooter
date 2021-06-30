using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SpawnerComponent : IComponentData
{
    //Prefab to spawn
    public Entity SpawnedPrefab;

    public float SpawnTimer;

    //Min (x) and max (y) time between asteroid instantiations
    public float2 TimerReset;

    //Min(x) and max(y) asteroid speed
    public float2 SpeedMinMax;
}
