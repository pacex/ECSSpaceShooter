using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct AsteroidSpawnerComponent : IComponentData
{
    //Prefab to spawn
    public Entity AsteroidPrefab;

    public float SpawnTimer;

    //Min (x) and max (y) time between asteroid instantiations
    public float2 TimerReset;

    //Min(x) and max(y) asteroid speed
    public float2 SpeedMinMax;
}
